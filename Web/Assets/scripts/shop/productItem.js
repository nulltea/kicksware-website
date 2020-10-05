import Viewer from "viewerjs"


function initCarousels() {
	$(".carousel-wrapper").each(function () {
		let carousel = $(this);
		carousel.find(".flickity-button").appendTo($(this));
		carousel.find(".flickity-page-dots .dot").detach();
	})
}

function favoriteInit() {
	$(".title-header .favorite-product input[type=checkbox]").change(function () {
		let id = window.location.pathname.split("/").slice(-1)
		let checked = $(this).is(":checked");
		$.get(`/shop/${checked ? "like" : "unlike"}/${id}`);
		$(this).parent().toggleClass("liked")
	})
	$(".carousel-cell .favorite-product input[type=checkbox]").change(function () {
		let id = $(this).closest(".carousel-cell").attr("id")
		let checked = $(this).is(":checked");
		$.get(`/shop/${checked ? "like" : "unlike"}/${id}`);
		$(this).parent().toggleClass("liked")
	})
}

function buyRedirectInit() {
	$(".buy-redirect").click(function (){
		$.get(`/Order/CommitOrder?referenceID=${$("#reference-id").val()}&productID=${$("#product-id").val() ?? ""}`, function(response) {
			if (!localStorage.getItem("kicksware.redirect-popup-never-again") && response.success && response.content) {
				$("#popup-dialog").html(response.content);
				window.redirectURL = response["redirectUrl"];
				showDialog();
				modalInit();
			}
		});
	})

	function showDialog() {
		$("#popup-modal").modal("show");
	}

	function closeDialog() {
		$("#popup-modal").fadeOut("slow").modal("hide");
	}

	function modalInit() {
		$(".never-show-again a").click(function (event){
			localStorage.setItem("kicksware.redirect-popup-never-again", "true")
			closeDialog();
			event.stopPropagation();
			event.preventDefault();
		})
	}
}

function photoGalleryInit() {
	if (window.isMobile()) {
		new Viewer($(".mobile-gallery .carousel-wrapper")[0], {
			toolbar: false,
			title: false,
			tooltip: false,
			minZoomRatio: 1,
			maxZoomRatio: 3,
		});
	}
	new Viewer($(".desktop-gallery .image-carousel")[0], {
		toolbar: false,
		title: false,
		tooltip: false,
		minZoomRatio: 1,
		maxZoomRatio: 3,
	});
}

$(document).ready(function () {
	"use strict";

	initCarousels();

	favoriteInit();

	buyRedirectInit();

	photoGalleryInit();
});