

$(document).ready(function () {

    function showLoader() {
        document.getElementById('overlay').style.display = 'block';
        document.getElementById('loader').style.display = 'block';
    }

    function hideLoader() {
        document.getElementById('overlay').style.display = 'none';
        document.getElementById('loader').style.display = 'none';
    }

    showLoader();

    setTimeout(function () {
        hideLoader();
        var contengrupoElement = document.getElementById('contengrupo');
        contengrupoElement.removeAttribute('style');
    }, 2000);



    $('#filtros').on('click', function (event) {
        event.preventDefault();
        var idTematica = document.getElementById('idfantastaSubtematica').value;

        var cardContairnerAssest = document.getElementById('cardContairnerAssest');
        cardContairnerAssest.removeAttribute('style');
        obtenerDatosGetByTematicYAgregarElemento(idTematica);

    });
    $('#filtrosAll').on('click', function (event) {
        obtenerDatosYAgregarElemento();

    });

    $(document).on('click', '#vertodos', function () {
        event.preventDefault();
        var elements = document.querySelectorAll('.show');
        var tematicaElement = document.getElementById('tematicatxt');
        var lblhidden = document.querySelectorAll('[id*="lblhidden"]');

        obtenerDatosYAgregarElemento();

        elements.forEach(function (element) {
            element.classList.remove('show');
        });

        tematicaElement.setAttribute('aria-expanded', 'false');


        // Itera sobre los elementos y elimina el atributo style
        lblhidden.forEach(function (element) {
            element.style.display = "none";
        });

    })

    $(document).on('click', '#subtematica', function () {
        event.preventDefault();
    })
    $(document).on('click', '#VerGrupos', function () {

        window.location.href = `/group/Index?`;

    })

    $(document).on('click', '#filtros', function () {
        $('#collapseExample').collapse('hide');

        var cardContairnerAssest = document.getElementById('cardContairnerAssest');
        cardContairnerAssest.removeAttribute('style');
    })

    $(document).on('click', '#tematicatxt', function () {
        var elements = document.querySelectorAll('[id*="lblhidden"]');

        // Itera sobre los elementos y elimina el atributo style
        elements.forEach(function (element) {
            element.removeAttribute("style");
        });
    })

});


function obtenerDatosGetByTematicYAgregarElemento(idTematica) {
    var idGrupo = window.location.pathname.split('/').pop();

    var url = `/api/Assets/byTematic?tematicId=${idTematica}&group=${idGrupo}`;
    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {          
            agregarAssest(data);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}


function obtenerDatosYAgregarElemento(idfiltro) {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `/api/Assets/getByGroupLow?id=${idGrupo}`;


    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {
            console.log("esta es la data");
            console.log(data);
            console.log(" fin esta es la data");
            agregarAssest(data, idfiltro);
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}



function obtenerDatosSubtematicasYAgregarElemento() {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `/api/Group/tematics?id=${idGrupo}`;

    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {
            
            agregarElementosTematicas(data);
            /* agregarAssest(data);*/
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
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
            <a href="#" class="tematicaid"  id="VerGrupos">
                <div>
                    <span>Grupos</span>
                </div>
            </a>
        </li>
        <li class="menu">
            <a href="#" class="tematicaid"  id="vertodos">
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
        nuevoLi.className = '';

        nuevoLi.innerHTML = `
                    <input id="group-${tematicaId}" type="checkbox" hidden />
                    <label for="group-${tematicaId}"  class="tematicaid" id="tematicatxt"  data-tematica-id-item="${tematicaId}" value="${tematicasArray[0]?.Nombre}">
                        <span  class="fa fa-angle-right"></span>${tematicasArray[0]?.Nombre || ''}
                    </label>

                    <ul class="group-list">
                        ${tematicasArray[0]?.Hijos.map(subtema => {
            const tieneNietos = tematicasArray[0]?.Nietos.some(nieto => nieto.ParentTematicaID === subtema.TematicaID);

            return tieneNietos ? `
                                <li>
                                    <input id="sub-group-${subtema.TematicaID}" type="checkbox" hidden />
                                    <label for="sub-group-${subtema.TematicaID}" data-tematica-id-item="${subtema.TematicaID}" value="${subtema.NombreTematica}" id="SubtematicaHijos">
                                        <span  class="fa fa-angle-right"></span>${subtema.NombreTematica}
                                    </label>
                                    <ul class="sub-group-list">
                                        ${tematicasArray[0]?.Nietos.filter(nieto => nieto.ParentTematicaID === subtema.TematicaID).map(nieto => `
                                            <li>
                                                <a href="#" data-tematica-id-item="${nieto.TematicaID}">${nieto.NombreTematica}</a>
                                            </li>`).join('')}
                                    </ul>
                                </li>
                            ` : `
                                <li>
                                    <label for="sub-group-${subtema.TematicaID}" data-tematica-id-item="${subtema.TematicaID}" value="${subtema.NombreTematica}" id="SubtematicaHijos">
                                        <span ></span>${subtema.NombreTematica}
                                    </label>

                                </li>`;
        }).join('')}
                    </ul>
         `;


        ul.appendChild(nuevoLi);
    }

    obtenerdatosnav();
}


function agregarAssest(data, idfiltro) {
    console.log(idfiltro);
    let imagenUrl;

    //if (idfiltro === "1") {
    //    imageUrl = "/Content/imagenes/rojo.png";
    //}
    //else if (idfiltro === "2") {
    //    imageUrl = "/Content/imagenes/Amarillo.png";

    //}
    //else if (idfiltro === "3") {
    //    imageUrl = "/Content/imagenes/rosado.png";

    //}
    //else if (idfiltro === "4") {
    //    imageUrl = "/Content/imagenes/VERDE.png";

    //}
   
    imagenUrl = "/Content/imagenes/libro1.png";
    const cardContainer = document.getElementById("cardContairnerAssest");
    cardContainer.innerHTML = "";

    if (idfiltro) {      

        const asset = data.Data.find(asset => asset.TipoAssetID === parseInt(idfiltro)); // Filtrar el asset que cumple con el filtro
        if (asset) {          
            
            const card = document.createElement("div");
            card.className = "col mb-4";
            card.id = "cardContenedor";          
            card.setAttribute("data-value", `${asset.TipoAssetID}`);

            // Crea la estructura de la tarjeta
            const imageSrc = asset.Image ? `data:image/png;base64,${asset.Image}` : "/Content/imagenes/Imagen.jpeg";
            card.innerHTML = `
             <div class="card">
                <div id="conteinerImg_${asset.TipoAssetID}" class="imgcontenedor"> <img src="${imagenUrl}" id="imgcard" class="card-img-top" alt="..."> </div>
                <div class="card-body">
                    <h5 class="card-title">${asset.Title} eee</h5>
                    <p class="card-text">${asset.Description}</p>
                    <a href='/group/ShowAssets?id=${asset.ID}' target="_blank" class="btn btn-primary">Ver contenido</a>
                </div>
            </div>
        `;
                   
            cardContainer.appendChild(card);
        }
    } else {
        data.Data.forEach(asset => {
            let imagenUrl;
            //if (asset.TipoAssetID === 1) {
            //    imagenUrl = "/Content/imagenes/rojo.png";
            //}
            //else if (asset.TipoAssetID === 2) {
            //    imagenUrl = "/Content/imagenes/Amarillo.png";

            //}
            //else if (asset.TipoAssetID === 3) {
            //    imagenUrl = "/Content/imagenes/rosado.png";

            //}
            //else if (asset.TipoAssetID === 4) {
            //    imagenUrl = "/Content/imagenes/VERDE.png";

            //}
            imagenUrl = "/Content/imagenes/libro1.png";

            const card = document.createElement("div");
            card.className = "col mb-4";
            card.id = "cardContenedor_";
            card.setAttribute("data-value", `${asset.TipoAssetID}`);

            // Crea la estructura de la tarjeta
            const imageSrc = asset.Image ? `data:image/png;base64,${asset.Image}` : "/Content/imagenes/Imagen.jpeg";
            card.innerHTML = `
            <div class="card">
                <div id="conteinerImg_${asset.TipoAssetID}" class="imgcontenedor"> <img src="${imagenUrl}" id="imgcard" class="card-img-top" alt="..."> </div>
                <div class="card-body">
                    <h5 class="card-title">${asset.Title} eee</h5>
                    <p class="card-text">${asset.Description}</p>
                    <a href='/group/ShowAssets?id=${asset.ID}' target="_blank" class="btn btn-primary">Ver contenido</a>
                </div>
            </div>
        `;
           
            cardContainer.appendChild(card);
        });
    }
}


document.addEventListener("DOMContentLoaded", function () {
    const filtros = document.querySelectorAll("#filtros");
    filtros.forEach(filtro => {
        filtro.addEventListener("click", function (event) {
            event.preventDefault();
            const idfiltro = filtro.getAttribute("data-value");
            console.log("este es el id del filtro ");
            console.log(idfiltro);
            obtenerDatosYAgregarElemento(idfiltro)
        });
    });
});

//function filtrarTarjetas(valorFiltro) {
//    const tarjetas = document.querySelectorAll("#cardContairnerAssest .card");
//    tarjetas.forEach(tarjeta => {
//        const tarjetaValue = tarjeta.getAttribute("data-value");
//        if (tarjetaValue === valorFiltro || valorFiltro === "all") {
//            tarjeta.style.display = "block";
//        } else {
//            tarjeta.style.display = "none";
//        }
//    });
//}




function obtenerDatosgetGruposYAgregarElemento() {
    var idGrupo = window.location.pathname.split('/').pop();
    var url = `/api/Group/id?id=${idGrupo}`;
    var nameGrupo = document.getElementById('Grupo');



    fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json"
        },
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {

            nameGrupo.textContent = `${data.Data.Description}`;

        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}

obtenerDatosYAgregarElemento();
obtenerDatosSubtematicasYAgregarElemento();
obtenerDatosgetGruposYAgregarElemento();




function obtenerdatosnav() {
    var ul = document.getElementById('accordionExample');
    var spanSubtematicasHeader = document.getElementById('subtematicasHeader');
    var spanUnidadHeader = document.getElementById('unidadHeader');
    var profesor = document.getElementById('profesor');
    var tematicaNameElements = document.querySelectorAll('#tematicatxt');
    var SubtematicaHijosElements = document.querySelectorAll('#SubtematicaHijos');

    function handleTematicaClick(element) {
        var tematicaValue = element.getAttribute('value');
        spanSubtematicasHeader.textContent = "";
        spanUnidadHeader.textContent = tematicaValue;
    }

    function handleSubtematicaClick(element) {
        var subtematicaValue = element.getAttribute('value');
        console.log(subtematicaValue);
        spanSubtematicasHeader.textContent = subtematicaValue;
    }

    function handleAccordionItemClick(element) {
        var subtematicaId = element.getAttribute('data-tematica-id-item');
        console.log(subtematicaId);

        profesor.textContent = 'Profesor Prueba';
        document.getElementById('idfantastaSubtematica').value = subtematicaId;

        obtenerDatosGetByTematicYAgregarElemento(subtematicaId);
    }

    ul.querySelectorAll('[data-tematica-id-item]').forEach((element) => {
        element.addEventListener('click', function () {
            handleAccordionItemClick(element);
        });
    });

    tematicaNameElements.forEach(function (element) {
        element.addEventListener('click', function () {
            handleTematicaClick(element);
        });
    });

    SubtematicaHijosElements.forEach(function (element) {
        element.addEventListener('click', function () {
            handleSubtematicaClick(element);
        });
    });
}

