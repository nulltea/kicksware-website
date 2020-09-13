let menuActive = false;

function initMenu() {
	let hamButton = $(".hamburger");
	let menu = $(".menu");

	hamButton.on("click", function (event) {
		$(this).toggleClass("open");
		event.stopPropagation();

		if (!menuActive) {
			openMenu();
		} else {
			closeMenu();
			menuActive = false;
		}
	});

	window.addEventListener("click", function (event) {
		if (menuActive && !isDescendant(menu[0], event.target)) {
			closeMenu();
			hamButton.toggleClass("open");
		}
	});

	//Handle page menu
	let items = $(".slider-menu-item");
	items.each(function () {
		let item = $(this);

		item.on("click", function (evt) {
			if (item.hasClass("has-children")) {
				evt.preventDefault();
				evt.stopPropagation();
				let subItem = item.find("> ul");
				if (subItem.hasClass("active")) {
					subItem.toggleClass("active");
					TweenMax.to(subItem, 0.3, {height: 0});
				} else {
					subItem.toggleClass("active");
					TweenMax.set(subItem, {height: "auto"});
					TweenMax.from(subItem, 0.3, {height: 0});
				}
			} else {
				evt.stopPropagation();
			}
		});
	});
}

function openMenu() {
	let menu = $(".menu");
	menu.addClass("active");
	menuActive = true;
}

function closeMenu() {
	$(".menu.active, .menu .active").each(function (){
		$(this).removeClass("active");
	});
	menuActive = false;
}

function goBackMenu() {
	$(".submenu-header button").click(function (){
		$(this).closest(".slider-menu-item.expandable").removeClass("active");
	});
}

function expandSumMenuHandle() {
	$(".slider-menu-item.expandable > a").click(function (evt) {
		evt.preventDefault();
		$(this).parent().toggleClass("active");
	})
}

$(document).ready(function () {
	"use strict";

	initMenu();

	expandSumMenuHandle();

	goBackMenu();
});