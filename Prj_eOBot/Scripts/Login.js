$(document).ready(function () {

    $(document).on('click', '#btnsuccesLogin', function () {
        Login();
    });

    $(document).on('click', '#btnsuccesCreate', function () {
        createUser();

    });

});


function Login() {
    var userName = $("#userName").val();
    var UserPassword = $("#userPassword").val();
    var url = "Login/LoginUser";
    let formData = {
        usermail: userName,
        userPassword: UserPassword
    };

    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {
            console.log(data);
            const userId = data.userId;
            if (data.StatusCode == 200) {
                //window.location.href = "/group/Index?userId=" + userId;
                window.location.href = "/Home/Index";
                console.log(userId);
            }
            else {
                alert("usuario no autorizado ");
            }
           
        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}


function createUser() {
    var Identificacion = $("#Identificacion").val();
    var Nombre = $("#Nombre").val();
    var Apellidos = $("#Apellidos").val();
    var perfil = $("#perfil").val();
    var Email = $("#Email").val();
    var password = $("#password").val();
    var ExpirationDate = "2024-12-31";
    var StatusID = 1;

    var url = "/api/User/Register";

    let formData = {
        ID: Identificacion,
        Name: Nombre,
        Surname: Apellidos,
        Profile: perfil,
        Email: Email,
        Password: password,
        ExpirationDate: ExpirationDate,
        StatusID: StatusID
    };

    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(formData)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Error en la solicitud. Código: " + response.status);
            }
            return response.json();
        })
        .then(data => {           
            if (data.StatusCode == 200) {
                alert("Usuario creado");
                window.location.href = "/Login/Index";
               
            }
            else {
                alert("usuario no creado");
            }

        })
        .catch(error => {
            console.error('Error en la solicitud:', error);
        });
}