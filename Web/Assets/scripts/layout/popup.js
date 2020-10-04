function popupInit() {
	window.addEventListener("click", function (event) {
		let modal = $("#popup-modal");
		if (modal.find(".popup-dialog").hasClass("locked")) {
			return;
		}
		if (modal.is(":visible") && !isDescendant(modal[0], event.target)) {
			closeDialog();
		}
	});

	function closeDialog() {
		$("#popup-modal").fadeOut("slow").modal("hide");
	}
}

function cookieConsentInit() {
	window.cookieconsent.initialise({
		"palette": {
			"popup": {
				"background": "#1d8a8a"
			},
			"button": {
				"background": "#62ffaa"
			}
		},
		"theme": "classic",
		"content": {
			"message": "",
			"href": ""
		}
	});
	let messageContent = $(
		"<span>This website uses cookies to ensure your best user experience. " +
		"By using <span class='kicksware'>Kicksware</span>, you accept our use of cookies.</span> " +
		"<a class='cc-link' href='https://kicksware.com/policy/cookie'>Learn more</a>"
	)
	$(".cc-message").html(messageContent)

}

function betweenPagesLoadInit(){
	$(window).on("beforeunload", function () {
		window.startLoadingOverlay(false);
	});
	$(window).on("load", function () {
		window.stopLoadingOverlay();
	});
}

$(document).ready(function () {
	"use strict";

	popupInit();

	cookieConsentInit();

	betweenPagesLoadInit();
});
