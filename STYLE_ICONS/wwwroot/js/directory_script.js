
const uri = "/api/catalogs/";
let item = null;

function getCatalogs() { //функция получения списка каталогов
    let request = new XMLHttpRequest();//создаем новый объект
    request.open("GET", uri); //конфигурация, где указываем путь к ресурсу и метод
    request.addEventListener("loadend", function () {//произойдет когда выполнится запрос
        item = "";
        let catalogsHTML = "";
        let catalogcreateHTML = "";
        item = JSON.parse(request.responseText);//ответ из строки в объект
        if (typeof item !== "undefined") {

            if (item.length > 0) {
                if (item) {
                    var i;
                    if (typeUser == "admin") {
                        for (i in item) {
                            catalogsHTML += '<li id = ' + item[i].Idcategory + ' class="catalogText"><span>' + item[i].Idcategory + ' : ' + '<a   id=' + item[i].Idcategory + "catalog" + ' onclick="choose_catalog(' + item[i].Idcategory + ')">' + item[i].CatalogName + "</a>" + ' </span>';
                            catalogsHTML += '<button id="ed" type="button" class="btn btn-outline-warning" onclick="editCatalog(' + item[i].Idcategory + ')" >Edit</button>';
                            catalogsHTML += '<button id="del" type="button" class="btn btn-outline-warning" onclick="deleteCatalog(' + item[i].Idcategory + ')">Delete</button></li>';
                        }
                        catalogsHTML += '<label id ="lcr" for="catalogcreate">' + "Название каталога:" + '</label>';
                        catalogsHTML += '<input id="catalogcreate" type="text"/>';
                        catalogsHTML += ' <button id ="bcr" type="button" class="btn btn-outline-warning" onclick="createCatalog()" >' + "Add" + '</button>';
                    }
                    else {
                        for (i in item) {
                            catalogsHTML += '<li id = ' + item[i].Idcategory + ' class="catalogText user"><span><a   id=' + item[i].Idcategory + 'catalog' + ' onclick="choose_catalog(' + item[i].Idcategory + ')">' + item[i].CatalogName + "</a>" + ' </span>';
                        }
                    }
                }
            }
            document.querySelector("#catalogsget").innerHTML = catalogsHTML;
            if (catalogcreateHTML !== "") document.querySelector("#catalogadd").innerHTML = catalogcreateHTML;
        }
    });
    request.send();//отправка запроса
}
function createCatalog() {//функция создания нового каталога
    let CatalogNameText = "";
    CatalogNameText = document.querySelector("#catalogcreate").value; //берем текст введенный в текстовое поле
    var request = new XMLHttpRequest();//создаем новый объект
    request.open("POST", uri);//конфигурация, где указываем путь к ресурсу и метод
    request.addEventListener("loadend", function () {//произойдет когда выполнится запрос
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для создания";
        } else if (request.status === 201) {
            msg = "Запись добавлена";
            getCatalogs();
        } else {
            msg = "Неизвестная ошибка";
        }
        $('#mNotification').modal('show');//выводим окно с сообщением
        document.querySelector("#msg3").innerHTML = msg;
        document.querySelector("#catalogcreate").value = "";
    });
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");//добавляем Content-Type, так как веб-API принимает запрос с данными формате JSON
    request.send(JSON.stringify({ CatalogName: CatalogNameText }));//отправка запроса
}

function editCatalog(id) {//функция изменение каталога
    document.getElementById("catalogsave").onclick = function () {//произойдёт при клике
        updateCatalog(id);
    }
    $('#EditCatalog').modal('show');//открытие окна с изменением
    document.getElementById("cataloginput").value = document.getElementById(id + "catalog").innerHTML;//заполняем текстовое поле исходными данными     
}


function updateCatalog(id) {//функция принимающая изменения
    $('#EditCatalog').modal('hide');
    const catalog = {
        Idcategory: id,
        ProductName: document.getElementById("cataloginput").value,
        ProductImage: document.getElementById("cataloginput").value,

    };
    var request = new XMLHttpRequest();//создаем новый объект
    var u = uri + catalog.Idcategory;
    request.open("PUT", u);//конфигурация, где указываем путь к ресурсу и метод
    request.addEventListener("loadend", function () {
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для изменения";
        } else if (request.status === 204) {
            document.getElementById(id + "catalog").innerHTML = catalog.CatalogName;
            msg = "Запись изменена";
        } else {
            msg = "Неизвестная ошибка";
        }
        $('#EditCatalog').modal('hide');//скрываем окно изменения
        $('#mNotification').modal('show');//выводим окно с сообщением
        document.querySelector("#msg3").innerHTML = msg;
    });
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify(catalog));
}



    function deleteCatalog(id) {//метод удаления каталога
        let request = new XMLHttpRequest();
        request.open("DELETE", uri + id, false);
        request.addEventListener("loadend", function () {
            // Обработка кода ответа
            var msg = "";
            if (request.status === 401) {
                msg = "У вас не хватает прав для удаления";
            } else if (request.status === 204) {
                $("#" + id).remove();
                msg = "Запись удалена";
            } else {
                msg = "Неизвестная ошибка";
            }
            $('#mNotification').modal('show');
            document.querySelector("#msg3").innerHTML = msg;
            getProduct();
        });
        
        request.send();
    }

