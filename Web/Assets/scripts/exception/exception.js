function errorDetailsInit() {
	$(".expandable").click(function (){
		$(".details-content").toggleClass("expanded");
		$("aside a").toggleClass("hidden");
	})
}

function tryAgainInit() {
	$("#try-again").click(function (){
		location.reload();
	});
}

$(document).ready(function () {
	"use strict";

	errorDetailsInit();

	tryAgainInit();
});