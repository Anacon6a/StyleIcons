var itm = "";

function choose_catalog(id) {//метод для запоминания на какой каталог нажал пользователь
    localStorage.setItem('idcatalog', id); //помещаем объект в локальное хранилище
    document.location = "directory_contents.html"; //переходим на страницу с каталогами
}
function getProduct() {//метод получения товаров
    let request = new XMLHttpRequest();
    request.open("GET", uri + localStorage.getItem('idcatalog'));//запрос выведет связанные данные
    request.addEventListener("loadend", function () {
        item = "";
        let productsHTML = "";
        let productcreateHTML = "";
        let namecatalogHTML = "";
        item = JSON.parse(request.responseText);
        if (typeof item !== "undefined") {
            namecatalogHTML += '<h3 class="nc"><img class="sh2" src="img/шнурок3.png">' + item[0].CatalogName + '</h3>';
            if (typeUser == "admin") {
                productsHTML += '<p class="pb"><button type="button" class="pcr btn btn-outline-warning" onclick="addnewProduct()" >Добавить товар</button></p>';
            }
            if (item[0].Product.length > 0) {
                if (item) {
                    var i, n = 0;
                    if (typeUser == "admin") {
                        //productsHTML += '<p class="pb"><button type="button" class="pcr btn btn-outline-warning" onclick="addnewProduct()" >Добавить товар</button></p>';
                        productsHTML += '<div class="row">';
                        for (i in item[0].Product) {

                            productsHTML += '<div class="col"><div class = "pr"><p class = "pri"><img class="imgpr" src=' + "img/" + item[0].Product[i].ProductImage + '></img></p><br>';
                            productsHTML += '<p class="pnpr">' + item[0].Product[i].ProductName + '</p><br>';
                            productsHTML += '<p class="pppr">' + item[0].Product[i].Price + " ₽" + '</p><br>';
                            productsHTML += '<p class="pqpr">' + item[0].Product[i].QuantityInStock + " шт." + '</p><br>';
                            productsHTML += '<p class="pbp"><button type="button" class="ped btn btn-outline-warning" onclick="editProduct(' + item[0].Product[i].Idproduct + ')" >Изменить</button>';
                            productsHTML += '<button type="button" class="pdel btn btn-outline-warning" onclick="deleteProduct(' + item[0].Product[i].Idproduct + ')">Удалить</button></p></div></div>';

                            n++;
                            if (i == item[0].Product.length - 1) {
                                
                                if (3 - n == 2) {
                                    productsHTML += '<div class="col"></div><div class="col"></div>';
                                }
                                productsHTML += '</div >';
                            }
                            else if (n == 2) {
                                productsHTML += '</div >';
                                productsHTML += '<div class="row">';
                            }
                            else if (n == 3) {
                                productsHTML += '</div >';
                                productsHTML += '<div class="row">';
                                n = 0;
                            }

                        }
                    }
                    else {
                        productsHTML += '<div class="row">';
                        for (i in item[0].Product) {

                            productsHTML += '<div class="col"><div class = "pr"><p class = "pri"><img class="imgpr" src=' + "img/" + item[0].Product[i].ProductImage + '></img></p><br>';
                            productsHTML += '<p class="pnpr">' + item[0].Product[i].ProductName + '</p><br>';
                            productsHTML += '<p class="pppr">' + item[0].Product[i].Price + " ₽" + '</p><br>';
                            productsHTML += '<p class="pbp"><button type="button" class="pad btn btn-outline-warning" onclick="createBasket(' + item[0].Product[i].Idproduct + ", " + item[0].Product[i].Price + ", " + item[0].Product[i].QuantityInStock + ')" >Добавить в  корзину</button></p></div></div>';

                            n++;
                            if (i == item[0].Product.length - 1) {

                                if (3 - n == 2) {
                                    productsHTML += '<div class="col"></div><div class="col"></div>';
                                }
                                productsHTML += '</div >';
                            }
                            else if (n == 2) {
                                productsHTML += '</div >';
                                productsHTML += '<div class="row">';
                            }
                            else if (n == 3) {
                                productsHTML += '</div >';
                                productsHTML += '<div class="row">';
                                n = 0;
                            }

                        }
                    }
                }
            }
            document.querySelector("#namecatalog").innerHTML = namecatalogHTML;
            document.querySelector("#productsget").innerHTML = productsHTML;
            if (productcreateHTML !== "") document.querySelector("#productadd").innerHTML = productgcreateHTML;
        }
    });
    request.send();
}

function addnewProduct() {//функция открывает окно для добавления товара
    $('#mProduct').modal('show');
    document.getElementById("mbp").innerHTML = '<button id="bAdd" onclick="createProduct()" type="button" class="btn w-100  text-white">Добавить</button>'
}

function createProduct() {//метод добавления товара
    document.getElementById("mprod").innerHTML = "Добавление товара";
    $('#mProduct').modal('hide');//скрываем модальное окно добавления товара
    const product = {
        Fkcategory: localStorage.getItem('idcatalog'),
        ProductName: document.querySelector("#fnameproduct").value,
        ProductImage: document.getElementById("fimgproduct").value,
        Price: document.querySelector("#lpriceproduct").value,
        QuantityInStock: document.querySelector("#mqproduct").value,
    }
    var request = new XMLHttpRequest();
    request.open("POST", "/api/products");
    request.addEventListener("loadend", function () {
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для создания";
        } else if (request.status === 201) {
            msg = "Запись добавлена";
            getProduct();
        } else {
            msg = "Неизвестная ошибка";
        }
        $('#mNotification').modal('show');
        document.querySelector("#msg3").innerHTML = msg;
        
    });
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify(product));
}

function editProduct(id) {//метод открывает окно для изменения информации о товаре
    document.getElementById("mprod").innerHTML = "Изменение товара";
    document.getElementById("mbp").innerHTML = '<button id="bSave" type="button" class="btn w-100  text-white">Сохранить</button>'
    document.getElementById("bSave").onclick = function () {
        updateProduct(id);
    }
    let request = new XMLHttpRequest();
    request.open("GET", "/api/products/" + id);
    request.addEventListener("loadend", function () {
        itm = JSON.parse(request.responseText);
        if (typeof itm !== "undefined") {
            if (itm) {//заполняем текстовые поля имеющимися данными
                if (id == itm.Idproduct) {
                    document.getElementById("fnameproduct").value = itm.ProductName;
                    document.getElementById("fimgproduct").value = itm.ProductImage;
                    document.getElementById("lpriceproduct").value = itm.Price;
                    document.getElementById("mqproduct").value = itm.QuantityInStock;
                }
            }
        }
        $('#mProduct').modal('show');
    });

    request.send();
}
   


function updateProduct(id) {//метод изменения информации о товаре
    $('#mProduct').modal('hide');
    var product = {
            Idproduct: id,
            Fkcategory: localStorage.getItem('idcatalog'),
            ProductName: document.querySelector("#fnameproduct").value,
            ProductImage: document.querySelector("#fimgproduct").value,
            Price: document.querySelector("#lpriceproduct").value,
            QuantityInStock: document.querySelector("#mqproduct").value,
        };
    var request = new XMLHttpRequest();
    request.open("PUT", "/api/products/" + id);
    request.addEventListener("loadend", function () {
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для изменения";
        } else if (request.status === 204) {
            getProduct();
            msg = "Запись изменена";
        } else {
            msg = "Неизвестная ошибка";
        }
        $('#mProduct').modal('hide');
        $('#mNotification').modal('show');
        document.querySelector("#msg3").innerHTML = msg;
    });
    request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
    request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    request.send(JSON.stringify(product));
}

function deleteProduct(id) {//метод удаления товара
    let request = new XMLHttpRequest();
    request.open("DELETE", "/api/products/" + id, false);
    request.addEventListener("loadend", function () {
        // Обработка кода ответа
        var msg = "";
        if (request.status === 401) {
            msg = "У вас не хватает прав для удаления";
        } else if (request.status === 204) {
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