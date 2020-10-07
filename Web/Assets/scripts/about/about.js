import ScrollMagic from "scrollmagic"
import "scrollmagic/scrollmagic/minified/plugins/debug.addIndicators.min"
import gsap from "gsap/dist/gsap"
import ScrollTrigger from "gsap/dist/ScrollTrigger"
import LocomotiveScroll from "locomotive-scroll"

const isMobile = window.isMobile();
const isMobileLocomotive = isMobile && requestScrollSetting();
const isLocomotive = !isMobile || isMobileLocomotive;
const scroller = isLocomotive ? "[data-scroll-container]" : "";



gsap.registerPlugin(ScrollTrigger)

function locomotiveScrollInit() {
	if (!isLocomotive) {
		return
	}

	const locoScroll = new LocomotiveScroll({
		el: document.querySelector("[data-scroll-container]"),
		smooth: true,
		smoothMobile: isMobileLocomotive
	});
	locoScroll.on("scroll", ScrollTrigger.update);
	ScrollTrigger.scrollerProxy("[data-scroll-container]", {
		scrollTop(value) {
			return arguments.length ? locoScroll.scrollTo(value, 0, 0) : locoScroll.scroll.instance.scroll.y;
		},
		getBoundingClientRect() {
			return {top: 0, left: 0, width: window.innerWidth, height: window.innerHeight};
		},
	});

	ScrollTrigger.addEventListener("refresh", () => locoScroll.update());

	ScrollTrigger.refresh();
}

function headerCorrectionInit() {
	if (isLocomotive === false) {
		return
	}

	let header = $("header");

	const controller = new ScrollMagic.Controller();
	new ScrollMagic.Scene({
		triggerElement: ".hero-content .bread",
		triggerHook: 0,
		offset: -300
	}).setClassToggle(".header", "scrolled")
		.addTo(controller);
}

function parallaxInit() {
	if (isLocomotive === false) {
		return
	}

	$(".title-section").each(function () {
		let cover = $(this).find(".section-cover")[0];
		let content = $(this).find(".section-content")[0];

		cover.style.backgroundPosition = `50% -${innerHeight / 3}px`
		gsap.to(cover, {
			backgroundPosition: `50% ${innerHeight / 3}px`,
			ease: "none",
			scrollTrigger: {
				trigger: this,
				scroller: scroller,
				scrub: true,
			},
		});

		gsap.to(content, {
			y: `100%`,
			ease: "none",
			scrollTrigger: {
				scroller: scroller,
				scrub: true,
				trigger: this,
			}
		});
	});

	$(".info-section").each(function () {
		let tl = gsap.timeline({
			scrollTrigger: {
				trigger: this,
				scroller: scroller,
				start: "top top",
				end: "bottom top",
				scrub: true,
			},
		});

		$(this).find("[data-scroll]").toArray().forEach(layer => {
			const depth = layer.dataset.depth;
			const movement = innerHeight * depth;
			let isMobileFigure = isMobile && layer.className === "figure" && window.matchMedia("(max-width: 870px)").matches;
			let verticalIndex = isMobileFigure ? layer.dataset.mobile_speed : 1;
			tl.to(layer, {
				y: movement * verticalIndex,
				ease: "none",
			}, 0)
		});
		let hand = $(this).find(".hand-wrapper")[0]
		if (hand) {
			gsap.to(hand, {
				x: hand.dataset.direction * 600,
				ease: "none",
				scrollTrigger: {
					trigger: hand,
					scroller: scroller,
					start: "bottom bottom",
					end: isMobile ? "" : "top top",
					duration: {min: 1, max: 3},
					scrub: true,
				},
			})
		}

		let creatorWidow = $(this).find("#creator-window")[0]

		if (creatorWidow) {

			gsap.to(creatorWidow, {
				x: isMobile ? "60vw" : undefined,
				y: isMobile ? "-100vh" : undefined,
				left: !isMobile ? "50%" : undefined,
				top: !isMobile ? "50%" : undefined,
				ease: "none",
				scrollTrigger: {
					trigger: isMobile ? "#mobile-bio-trigger" : "#bio-trigger",
					scroller: scroller,
					start: "top center",
					duration: {min: 3, max: 6},
					end: "top 100px",
					scrub: true,
				},
			});
		}
	});

	$(".contact-section").each(function (){
		gsap.to(this, {
			scrollTrigger: {
				trigger: this,
				scroller: scroller,
				start: "-=600",
				scrub: true,
				once: true,
				toggleClass: "higher"
			}
		})

		if (window.isMobile()) {
			let squares = $(this).find(".mobile.square");
			squares.toArray().forEach(figure => {
				gsap.to(figure, {
					marginTop: "130vh",
					ease: "none",
					scrollTrigger: {
						trigger: this,
						scroller: scroller,
						scrub: true,
					}
				});
			})
		} else {
			let circle = $(this).find(".circle")[0];
			let rectangle = $(this).find(".rectangle")[0];

			gsap.to(circle, {
				translateX: "200vw",
				ease: "none",
				scrollTrigger: {
					trigger: this,
					scroller: scroller,
					scrub: true,
				}
			});

			gsap.to(rectangle, {
				translateX: "185vw",
				ease: "none",
				scrollTrigger: {
					trigger: this,
					scroller: scroller,
					scrub: true,
				}
			});
		}
	});
	// setTimeout(function (){
	// 	$(".contact-section").css("height", "85vh");
	// }, 2000)
}

function requestScrollSetting() {
	return window.getCookie("EXPERIMENTAL_SCROLL_COOKIE") === "True";
}

function mobileParallaxInit() {
	if (isLocomotive) {
		return
	}

	$(".mobile.square").css("margin-top", "65vh");
	$(".contact-section").css("height", "85vh");
	$(".section-content").css("top", 0);

	const controller = new ScrollMagic.Controller();

	$(".info-section").each(function () {
		let hand = $(this).find(".hand-wrapper")
		let trigger = $(this).find("article p:last-child")
		new ScrollMagic.Scene({
			triggerElement: trigger[0],
			offset: trigger.height(),
		}).setClassToggle(hand[0], "pushed")
			.addTo(controller);
		hand.css("margin-bottom", 0)
	})

	new ScrollMagic.Scene({
		triggerElement: "#mobile-bio-trigger",
	}).setClassToggle("#creator-window", "active")
		.addTo(controller);

	window.heroParallaxInit();
}

$(document).ready(function () {
	"use strict";

	locomotiveScrollInit();

	headerCorrectionInit();

	parallaxInit();

	mobileParallaxInit();
});

