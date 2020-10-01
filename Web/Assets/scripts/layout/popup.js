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

$(document).ready(function () {
	"use strict";

	popupInit();
});
