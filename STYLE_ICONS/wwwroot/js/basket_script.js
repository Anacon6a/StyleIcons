let orders;


//получаем корзину
function getBasket() {
	
	if (typeUser == "") {

	}
	else {
		//получаем заказы
		let request = new XMLHttpRequest();
		request.open("GET", "/api/orders/?fk=" + iduser);
		request.addEventListener("loadend", function () {
			orders = "";
			orders = JSON.parse(request.responseText);
			if (orders != undefined) {
				fkorder = Search(1); //ищем заказ с нужным статусом
				
			}
			if (fkorder != 0) { //если несформированный заказ есть у пользователя
				getBaskets(fkorder);
			}
			else {
				document.querySelector("#addbasket").innerHTML = '<h3 id="notBasket">Корзина пуста</h3>';
				$('#mBasket').modal('show');
			}
		});
		request.send();
	}
}


//получение корзины
function getBaskets(fkorder) {
	let request = new XMLHttpRequest();
	request.open("GET", "/api/baskets/?fk=" + fkorder);
	request.addEventListener("loadend", function () {

		var basket = JSON.parse(request.responseText);
		if (typeof basket !== "undefined") {

			getProductinBasket(basket)
		}

	});
	request.send();
}
//получение товаров в корзине
function getProductinBasket(basket) {

	let costHTML = "";
	for (i in orders) {
		if (orders[i].Idorder == fkorder) {
			costHTML = '<h3 id="notBasket">Стоимость: ' + orders[i].Cost + ' ₽</h3><br>';
		}
	}
	if (costHTML != undefined) {
		document.querySelector("#addbasket").innerHTML = costHTML;
	}
	$('#mBasket').modal('show');
	
	for (i in basket) {
		let request = new XMLHttpRequest();
		request.open("GET", "/api/products/" + basket[i].Fkproduct);
		request.addEventListener("loadend", function () {
			let HTML = "";
			let item = "";
			let q = "";
			item = JSON.parse(request.responseText);
			for (i in basket) {
				if (basket[i].Fkproduct == item.Idproduct)
					q = basket[i].Quantity;
			}
			if (item != undefined) {
				HTML += '<div class="col"><div class = "center-block divproduct"><p class = "pproduct"><img class="imgproduct" src=' + "img/" + item.ProductImage + '></img></p><br>';
				HTML += '<p class="ppproduct">' + item.ProductName + '</p><br>';
				HTML += '<p class="ppproduct">' + item.Price + " ₽" + '</p><br>';
				HTML += '<p class="ppproduct">' + "Количество: " + q + " шт." + '</p><br>';
				document.querySelector("#addbasket").innerHTML += HTML;
			}

		});
		request.send();
	}

}

//добавление товара в корзину
function createBasket(fk, price, quantity) {
	if (quantity == 0) {
		$('#mNotification').modal('show');
		document.querySelector("#msg3").innerHTML = "Товара нет в наличии";
		return;
	}
	if (typeUser == "") {//если пользователь не зашел в систему

		var baskets = JSON.parse(localStorage.getItem("baskets"));
		if (baskets != undefined) {
			var quantity = JSON.parse(localStorage.getItem("quantity"));
			var cost = JSON.parse(localStorage.getItem("cost"));
			var key = Object.keys(baskets).find(key => baskets[key] === fk);
			quantity[key] += 1;
			cost += price;
		}
		else {
			baskets.unshift(fk);
			quantity.unshift(1);

		}
		
		localStorage.setItem(array, JSON.stringify(baskets));
		localStorage.setItem(array, JSON.stringify(quantity));
	}
	else {//если пользователь авторизировался
		var fkorder = 0;
		//получаем заказы
		let request = new XMLHttpRequest();
		request.open("GET", "/api/orders/?fk=" + iduser);
		request.addEventListener("loadend", function () {
			
				orders = "";
				orders = JSON.parse(request.responseText);
				if (orders != undefined) {
					fkorder = Search(1); //ищем заказ с нужным статусом
				}
				if (fkorder != 0) { //если несформированный заказ есть у пользователя
					
					AddBasket(fk, fkorder, price);
				}
				else {
					createOrder(fk, price);//добавить несформированный заказ
				}
			
		});
		request.send();
		
	}
}


//создание несформированного заказа
function createOrder(fk, price) {
	const order = {
		Fkuser: iduser,
		Fkstatus: 1,
		Address: "",
		Cost: 0,
	}
	var request = new XMLHttpRequest();
	request.open("POST", "/api/orders");
	request.addEventListener("loadend", function () {
		orders = "";
		orders = JSON.parse(request.responseText);
		if (orders != undefined) {
			fkorder = Search(1); //ищем заказ с нужным статусом
		}
		if (fkorder != 0) { //если несформированный заказ есть у пользователя
			
			AddBasket(fk, fkorder, price);
		}
		else {
			createOrder(fk, price);//добавить несформированный заказ
		}
	});
	request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.send(JSON.stringify(order));
}

//добавление товара в корзину
function AddBasket(fk, fkorder, price) {

	var basket = {
		Fkorder: fkorder,
		Fkproduct: fk,
		Quantity: 1,
		Price: price,
	}
	var request = new XMLHttpRequest();
	request.open("POST", "/api/baskets");
	request.addEventListener("loadend", function () {


	});
	request.setRequestHeader("Accepts", "application/json;charset=UTF-8");
	request.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
	request.send(JSON.stringify(basket));
}


//поиск заказов по статусу
function Search(orderstatus) {
	
	for (i in orders) {
		if (orders[i].Fkstatus == orderstatus) {
			return orders[i].Idorder
		}
	}
	return 0;
	}


