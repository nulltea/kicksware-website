import gsap from "gsap/dist/gsap"
import ScrollTrigger from "gsap/dist/ScrollTrigger"
import LocomotiveScroll from "locomotive-scroll"
import "locomotive-scroll/dist/locomotive-scroll.css"

const isMobile = window.isMobile();

gsap.registerPlugin(ScrollTrigger)

function locomotiveScrollInit() {
	const locoScroll = new LocomotiveScroll({
		el: document.querySelector("[data-scroll-container]"),
		smooth: true,
		smoothMobile: false
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
	let header = $("header")[0]

	gsap.to(header, {
		scrollTrigger: {
			trigger: "header",
			toggleClass: "scrolled",
			start: "+=210",
			endTrigger:"html",
			end:"bottom top",
			scroller: "[data-scroll-container]",
			scrub: true,
		}
	})
}

function parallaxInit() {
	$(".title-section").each(function () {
		let cover = $(this).find(".section-cover")[0];
		let content = $(this).find(".section-content")[0];

		cover.style.backgroundPosition = `50% -${innerHeight / 3}px`
		gsap.to(cover, {
			backgroundPosition: `50% ${innerHeight / 3}px`,
			ease: "none",
			scrollTrigger: {
				trigger: this,
				scroller: "[data-scroll-container]",
				scrub: true,
			},
		});

		gsap.to(content, {
			y: `100%`,
			ease: "none",
			scrollTrigger: {
				scroller: "[data-scroll-container]",
				scrub: true,
				trigger: this,
			}
		});
	});

	$(".info-section").each(function () {
		let tl = gsap.timeline({
			scrollTrigger: {
				trigger: this,
				scroller: "[data-scroll-container]",
				start: "top top",
				end: "bottom top",
				scrub: true,
			},
		});

		$(this).find("[data-scroll]").toArray().forEach(layer => {
			const depth = layer.dataset.depth;
			const movement = innerHeight * depth;
			let isMobileFigure = isMobile && layer.className === "figure" && window.matchMedia("(max-width: 870px)").matches;
			let verticalIndex = isMobileFigure ? 5 : 1;
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
					scroller: "[data-scroll-container]",
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
				left: "50%",
				top: isMobile ? "25%" : "50%",
				ease: "none",
				scrollTrigger: {
					trigger: isMobile ? "#mobile-bio-trigger" : "#bio-trigger",
					scroller: "[data-scroll-container]",
					start: "top center",
					duration: {min: 3, max: 6},
					end: "top 100px",
					scrub: true,
				},
			});
		}
	});

	$(".contact-section").each(function (){

		if (window.isMobile()) {
			let squares = $(this).find(".mobile.square");
			squares.toArray().forEach(figure => {
				gsap.to(figure, {
					marginTop: "130vh",
					ease: "none",
					scrollTrigger: {
						trigger: this,
						scroller: "[data-scroll-container]",
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
					scroller: "[data-scroll-container]",
					scrub: true,
				}
			});

			gsap.to(rectangle, {
				translateX: "200vw",
				ease: "none",
				scrollTrigger: {
					trigger: this,
					scroller: "[data-scroll-container]",
					scrub: true,
				}
			});
		}
	});
	setTimeout(function (){
		$(".contact-section").css("height", "85vh");
	}, 1000)
}

$(document).ready(function () {
	"use strict";

	locomotiveScrollInit();

	headerCorrectionInit();

	parallaxInit();
});

