var typeUser = "";//тип юзера
var username = "";//имя юзера
var iduser = "";//id 
var docname = "";//текущая страница
//// Обработка клика по кнопке регистрации
document.querySelector("#registerBtn").addEventListener("click", Register);
document.getElementById("loginBtn").addEventListener("click", logIn);

function Register() {
    // Считывание данных с формы
    var firstname = document.querySelector("#fnameginput").value;
    var lastname = document.querySelector("#lnameginput").value;
    var middlename = document.querySelector("#mnameginput").value;
    var phonenumber = document.querySelector("#pnumberinput").value;
    var email = document.querySelector("#emailinput").value;
    var password = document.querySelector("#passinput").value;
    var passwordConfirm = document.querySelector("#passcinput").value;
    let request = new XMLHttpRequest();
    request.open("POST", "/api/Account/Register");
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    // Обработка ответа
    request.onload = function () {
        ParseResponse(this);
    };
    // Запрос на сервер
    request.send(JSON.stringify({
        firstname: firstname,
        lastname: lastname,
        middlename: middlename,
        phonenumber: phonenumber,
        email: email,
        password: password,
        passwordConfirm: passwordConfirm
    }));
}

// Разбор ответа
function ParseResponse(e) {
    // Очистка контейнера вывода сообщений
    document.querySelector("#msg").innerHTML = "";
    var formError = document.querySelector("#formError");
    while (formError.firstChild) {
        formError.removeChild(formError.firstChild);
    }
    // Обработка ответа от сервера
    let response = JSON.parse(e.responseText);
    //document.querySelector("#msg").innerHTML = response.message;
    // Вывод сообщений об ошибках
    if (typeof response.error !== "undefined") {
        document.querySelector("#msg").innerHTML = response.error;
        if (typeof response.allerror !== "undefined" && response.allerror.length > 0) {
            for (var i = 0; i < response.allerror.length; i++) {
                let ul = document.querySelector("#formError");
                let li = document.createElement("li");
                li.appendChild(document.createTextNode(response.allerror[i]));
                ul.appendChild(li);
            }
        }
    }
    else {
        $('#mRegister').modal('hide');
        $('#mNotification').modal('show');
        response = JSON.parse(e.responseText);
        document.getElementById("msg3").innerHTML = response.message;
        //getCurrentUser();
    }
    // Очистка полей паролей
    document.querySelector("#passinput").value = "";
    document.querySelector("#passcinput").value = "";
}



function logIn() {

    // Считывание данных с формы
    var login = document.getElementById("loginput").value;
    var password = document.getElementById("logpassinput").value;
    var rememberme = document.getElementById("remembermech").checked;
    var request = new XMLHttpRequest();
    request.open("POST", "/api/Account/Login");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.onreadystatechange = function () {
        // Очистка контейнера вывода сообщений
        document.getElementById("msg2").innerHTML = "";
        var mydiv = document.getElementById('formError2');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        // Обработка ответа от сервера
        if (request.responseText !== "") {
            var msg = JSON.parse(request.responseText);
            //document.getElementById("msg2").innerHTML = msg.message;
            
            // Вывод сообщений об ошибках
            if (typeof msg.error !== "undefined") {
                document.getElementById("msg2").innerHTML = msg.error;
                if (typeof msg.allerror !== "undefined" && msg.allerror.length > 0) {
                    for (var i = 0; i < msg.allerror.length; i++) {
                        var ul = document.querySelector("#formError2");
                        var li = document.createElement("li");
                        li.appendChild(document.createTextNode(msg.allerror[i]));
                        ul.appendChild(li);
                    }
                }
            }
            else {
                $('#mLogin').modal('hide');
                $('#mNotification').modal('show');
                msg = JSON.parse(request.responseText);
                document.getElementById("msg3").innerHTML = msg.message;
                getCurrentUser();
        
            }
            document.getElementById("logpassinput").value = "";
        }

    };
    // Запрос на сервер
    request.send(JSON.stringify({
        login: login,
        password: password,
        rememberme: rememberme
    }));
}

function logOff() {
    var request = new XMLHttpRequest();
    request.open("POST", "api/account/logoff");
   
    request.addEventListener("loadend", function () {
        var msg = JSON.parse(this.responseText);
        document.getElementById("msg").innerHTML = "";
        var mydiv = document.getElementById('formError');
        while (mydiv.firstChild) {
            mydiv.removeChild(mydiv.firstChild);
        }
        $('#mNotification').modal('show');
        document.getElementById("msg3").innerHTML = msg.message;
        getCurrentUser();
    });
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send();
}


function getCurrentUser() {//метод проверкии сессии пользователей
    let request = new XMLHttpRequest();
    docname = document.location;//возвращает текущий документ
    request.open("POST", "/api/Account/isAuthenticated", true);
    request.addEventListener("loadend", function () {
        let myObj = "";
        myObj = request.responseText !== "" ? JSON.parse(request.responseText) : {};
        //на разных страницах выводит полученное сообщение
        if (docname == "https://localhost:44367/" || docname == "https://localhost:44367/index.html") {
            document.getElementById("acc").innerHTML = myObj.message;
        }
        if (docname == "https://localhost:44367/directory_contents.html") {
            getProduct();
        }
        //если пользователь зашел в систему заполняет данными
        username = myObj.name !== null ? myObj.name : "";
        typeUser = myObj.type !== null ? myObj.type[0] : "";
        iduser = myObj.type !== null ? myObj.idu : "";
        loading();
        getCatalogs();
    });
    request.send();
}


function loading() {//метод загружает различные данные в зависимости от того, зашел ли пользователь
    docname = document.location;
    let HTML = "";
    if (docname == "https://localhost:44367/" || docname == "https://localhost:44367/index.html") {
        if (typeUser == "") {
            HTML += '<a href="#mLogin" data-toggle="modal">' + "- Войти в личный кабинет" + '</a><br>';
            HTML += '<a href="#mRegister" data-toggle="modal">' + "- Регистрация" + '</a><br>';
            HTML += '<a id="bas" onclick="getBasket()" >' + "- Корзина" + '</a><br>';
        }
        else {
            HTML += '<a href="#lllll" data-toggle="modal">' + "- Личный кабинет" + '</a><br>';
            HTML += '<a id="bas" onclick="getBasket()" >' + "- Корзина" + '</a><br>';
            HTML += '<a id="hovloff" onclick="logOff()">' + "- Выйти" + '</a>';
        }
        document.querySelector("#persAcc").innerHTML = HTML;
    }
    else if (docname == "https://localhost:44367/directory_contents.html") {
        if (typeUser == "") {
            HTML += '<li class="lg"><a href="#mLogin" data-toggle="modal">' + "Войти" + '</a></li><br>';
            HTML += '<li class="lg"><a href="#mRegister" data-toggle="modal">' + "Зарегистрироваться" + '</li></a>';
        }
        else {
            HTML += '<li class="lg"><a href="#" >' + username + '</a></li><br>';
            HTML += '<li class="lg"><a onclick="logOff()">' + "Выйти" + '</a></li>';
        }
        document.querySelector("li.log").innerHTML = HTML;
    }
}