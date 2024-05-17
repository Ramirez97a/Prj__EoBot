function agregarElementosTematicas(data) {
    const ul = document.getElementById('accordionExample');

    // Limpiar el contenido actual de la lista
    ul.innerHTML = '';

    // Agregar el contenido HTML antes del bucle forEach
    const contenidoInicial = `
        <li class="menu active ">
            <a href="#" class="dropdown-toggle" id="btnhome">
                <div>
                    <img src="/Content/imagenes/Logo_inicio.png" alt="logo" width="30px" height="30px" style="margin-right:40px;"><span>PIMAS</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="dropdown-toggle" id="VerGrupos">
                <div>
                    <span>Grupos</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="dropdown-toggle" id="vertodos">
                <div>
                    <span>Todos los Archivos</span>
                </div>
            </a>
        </li>
    `;
    ul.innerHTML += contenidoInicial;

    const tematicasAgrupadas = {};

    data.Data.forEach((item) => {
        const { NombreTematica, ParentTematicaID, TematicaID, TematicaPadre } = item;

        if (NombreTematica && ParentTematicaID !== null) {
            tematicasAgrupadas[ParentTematicaID] = tematicasAgrupadas[ParentTematicaID] || [];
            tematicasAgrupadas[ParentTematicaID].push({ NombreTematica, TematicaID, TematicaPadre });
        }
    });

    // Recorrer el objeto y agregar las temáticas al DOM
    for (const [tematicaId, tematicasArray] of Object.entries(tematicasAgrupadas)) {
        const nuevoLi = document.createElement('li');
        nuevoLi.className = 'menu';

        const tematicaPadreNombre = tematicasArray[0]?.TematicaPadre?.NombreTematica || '';

        nuevoLi.innerHTML = `
            <a href="#tematica_${tematicaId}" data-bs-toggle="collapse" aria-expanded="false" class="dropdown-toggle" id="tematicatxt" value="${tematicaId}">
                <div>
                    <span id="idUnidad">Unidad: ${tematicaPadreNombre}</span>
                </div>
                <div>
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-right"><polyline points="9 18 15 12 9 6"></polyline></svg>
                </div>
            </a>
            <ul class="collapse submenu list-unstyled" id="tematica_${tematicaId}" data-bs-parent="#accordionExample">
                ${tematicasArray.map(({ NombreTematica, TematicaID }) => `<li><a href="component_tabs.html"  data-tematica-id-item="${TematicaID}"  id="subtematica">${NombreTematica}</a></li>`).join('')}
            </ul>
        `;
        ul.appendChild(nuevoLi);
    }

    // Agregar evento para gestionar el estado de los elementos
    ul.querySelectorAll('[data-bs-toggle="collapse"]').forEach((element) => {
        element.addEventListener('click', function () {
            const collapseTarget = document.querySelector(element.getAttribute('href'));
            ul.querySelectorAll('.collapse.show').forEach((collapse) => {
                if (collapse !== collapseTarget) {
                    collapse.classList.remove('show');
                }
            });
        });
    });
    obtenerdatosnav();
}








function agregarElementosTematicas(data) {
    const ul = document.getElementById('accordionExample');
    ul.innerHTML = ''; // Limpiar el contenido actual

    const contenidoInicial = `
       <li class="menu active ">
            <a href="#" class="dropdown-toggle" id="btnhome">
                <div>
                    <img src="/Content/imagenes/Logo_inicio.png" alt="logo" width="30px" height="30px" style="margin-right:40px;"><span>PIMAS</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="dropdown-toggle" id="VerGrupos">
                <div>
                    <span>Grupos</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="dropdown-toggle" id="vertodos">
                <div>
                    <span>Todos los Archivos</span>
                </div>
            </a>
        </li>
    `;
    ul.innerHTML += contenidoInicial;

    const tematicasAgrupadas = {};

    data.Data.forEach((item) => {
        const { Id, Nombre, Hijos, Nietos } = item;

        if (Nombre && Hijos.length > 0) {
            tematicasAgrupadas[Id] = tematicasAgrupadas[Id] || [];
            tematicasAgrupadas[Id].push({ Nombre, Hijos, Nietos });
        }
    });

    for (const [tematicaId, tematicasArray] of Object.entries(tematicasAgrupadas)) {
        const nuevoLi = document.createElement('li');
        nuevoLi.className = 'menu';

        nuevoLi.innerHTML = `
            <a href="#tematica_${tematicaId}" data-bs-toggle="collapse" aria-expanded="false" class="dropdown-toggle" id="tematicatxt" value="${tematicasArray[0]?.Nombre || ''}">
                <div>
                    <span id="idUnidad">Unidad: ${tematicasArray[0]?.Nombre || ''}</span>
                </div>
                <div>
                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-right"><polyline points="9 18 15 12 9 6"></polyline></svg>
                </div>
            </a>
            <ul class="collapse submenu list-unstyled" id="tematica_${tematicaId}" data-bs-parent="#accordionExample">
                ${tematicasArray[0]?.Hijos.map(subtema => `
                    <li>
                        <a href="#" data-tematica-id-item="${subtema.TematicaID}" class="subtematica">
                            <div>
                                <span id="SubtematicaHijos">${subtema.NombreTematica}</span>
                            </div>
                            ${tematicasArray[0]?.Nietos.filter(nieto => nieto.ParentTematicaID === subtema.TematicaID).length > 0 ? `
                                <div>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-chevron-right">
                                        <polyline points="9 18 15 12 9 6"></polyline>
                                    </svg>
                                </div>
                            ` : ''}
                        </a>
                        <ul class="collapse submenu-nietos" id="nieto_${subtema.TematicaID}" style="padding-left: 0px;">
                            ${tematicasArray[0]?.Nietos.filter(nieto => nieto.ParentTematicaID === subtema.TematicaID).map(nieto => `
                                <a href="#" data-tematica-id-item="${nieto.TematicaID}" data-bs-toggle="collapse" aria-expanded="false" class="dropdown-toggle">
                                    <div>
                                        <span id="subtematicasNietos">${nieto.NombreTematica}</span>
                                    </div>
                                </a>`).join('')}
                        </ul>
                    </li>`).join('')}
            </ul>
        `;
        ul.appendChild(nuevoLi);
    }

    // Agregar eventos para abrir/cerrar los collapses al hacer clic en los enlaces
    ul.querySelectorAll('.submenu .dropdown-toggle').forEach((element) => {
        element.addEventListener('click', function () {
            const collapseTarget = document.querySelector(element.getAttribute('href'));
            collapseTarget.classList.toggle('show');
        });
    });

    ul.querySelectorAll('.submenu .subtematica').forEach((element) => {
        element.addEventListener('click', function (event) {
            event.preventDefault();
            const collapseTarget = document.querySelector(`#nieto_${element.getAttribute('data-tematica-id-item')}`);
            collapseTarget.classList.toggle('show');
        });
    });

    obtenerdatosnav();
}
