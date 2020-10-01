import {gsap, TweenMax, Elastic} from "gsap/dist/gsap"
import $ from "jquery";

function sidebarControlInit() {
	$(".profile-sidebar input[type=button]").click(function () {
		let newActive = $(`#section-${this.id}`)
		if (newActive.length) {
			$(".profile-section.active").toggleClass("active");
			newActive.toggleClass("active");
			let mode = newActive.attr("name")
			if (window.history)
			window.history.replaceState("Kicksware", `(Page ${mode})`, `/profile/${mode}`);
		}
	})
	let mode = location.pathname.split("/")[2]
	if (mode) {
		$(".profile-section.active").toggleClass("active");
		$(`section[name=${mode}]`).toggleClass("active");
	}
	$(".profile-sidebar nav li").click(function (){
		$(this).find("label").click();
	})
	$(".profile-sidebar nav svg").click(function (){
		$(this).find("~ label").click();
	})
}

function profileFormInit(){
	let form = $(".profile-form");

	form.submit(function (event) {
		event.preventDefault();
		$.post(form.attr("action"), form.serialize(), function(response) {
			showAlert(response.result, response.message);
		});
	})
}

function showAlert(mode, message, lifetime = 5) {
	resetAlert(function () {
		$(".profile .alert-banner")
			.addClass(mode)
			.text(message)
			.addClass("active")
		clearTimeout(window.lifetimeHandler)
		window.lifetimeHandler = window.setTimeout(function () {
			resetAlert();
		}, lifetime * 1000);
	});
}


function resetAlert(callback) {
	let banner = $(".profile .alert-banner");
	if (callback) {
		if (banner.hasClass("active")){
			requestAnimationFrame(function () {
				banner.removeClass("active success error warning").text("");
			})
			window.setTimeout(callback, 500);
		}
		callback();
	} else {
		banner.removeClass("active success error warning").text("");
	}
}

function favouriteInit(){
	loading($(".product-cell"))
	$(".product-cell .favorite input[type=checkbox]").change(function () {
		let id = $(this).closest(".product-cell").attr("id")
		let checked = $(this).is(":checked");
		$.get(`/shop/${checked ? "like" : "unlike"}/${id}`);
		$(this).parent().toggleClass("liked")
		if (!$(this).is(":checked")) {
			let cell = $(this).closest(".product-cell");
			requestAnimationFrame(function () {
				cell.css("transform", "scale(0)")
			})
			window.setTimeout(function () {
				cell.remove();
			}, 300);
		}
	})
}

function loading(items){
	TweenMax.staggerFrom(items, 1, {
		scale: 0.6,
		opacity: 0,
		delay: .5,
		ease: Elastic.easeOut,
		force3D: true,
		clearProps: "all"
	}, 0.05);
}

function themeSettingInit(){
	$("#theme-select ~ .select-items div").click(function () {
		let theme = $(this).text().toLowerCase()
		$("body").removeClass("theme-light theme-dark").addClass(`theme-${theme}`);
		$(".theme").removeClass("light dark").addClass(theme);
	})
}

$(document).ready(function () {
	"use strict";

	sidebarControlInit();

	profileFormInit();

	favouriteInit();

	themeSettingInit();
});