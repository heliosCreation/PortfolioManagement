
(() => {
    $('.scroll').on("click", function (e) {
        e.preventDefault();

        var dest = $(this).attr("href");

        $("html, body").animate({

            'scrollTop': $(dest).offset().top

        }, 2000);

    });
});
/***************Navigation menu***********************/
(() => {
    const hamburgerMenu = document.querySelector(".hamburger-btn"),
        navMenu = document.querySelector(".nav-menu"),
        closeNavBtn = document.querySelector(".close-nav-menu");

    hamburgerMenu.addEventListener("click", toggleMenu)
    closeNavBtn.addEventListener("click", toggleMenu)

    function toggleMenu() {
        navMenu.classList.toggle("open");
        fadeOutEffect()
        bodyStopScrollingToggle()
    }

    function fadeOutEffect() {
        document.querySelector(".fade-out-effect").classList.add("active");
        setTimeout(() => {
            document.querySelector(".fade-out-effect").classList.remove("active");
        }, 300)
    }

    /**Document event handler**/
    document.addEventListener("click", (event) => {
        if (event.target.classList.contains("link-item")) {
            if (event.target.hash !== "") {
                event.preventDefault();
                const hash = event.target.hash;

                //document.querySelector(".section.active").classList.add("hide");
                //document.querySelector(".section.active").classList.remove("active");

                //document.querySelector(hash).classList.add("active");
                //document.querySelector(hash).classList.remove("hide");

                navMenu.querySelector(".active").classList.add("outer-shadow", "hover-in-shadow");
                navMenu.querySelector(".active").classList.remove("active", "inner-shadow");

                if (navMenu.classList.contains("open")) {

                    event.target.classList.add("active", "inner-shadow");
                    event.target.classList.remove("outer-shadow", "hover-in-shadow");

                    toggleMenu();
                }
                else {
                    let navItems = navMenu.querySelectorAll(".link-item");
                    navItems.forEach((item) => {
                        if (hash === item.hash) {

                            item.classList.add("active", "inner-shadow");
                            item.classList.remove("outer-shadow", "hover-in-shadow");
                        }
                    })
                    fadeOutEffect();
                }

                window.location.hash = hash;
            }
        }
    })
})();

/*********** Theme *************/

//(() => {
//    const themeChanger = document.querySelector(".header .theme"),
//        icon = themeChanger.querySelector("i"),
//        body = document.querySelector("body");

//    themeChanger.addEventListener("click", () => {
//        themeChanger.querySelector("i").classList.toggle("fa-moon")
//        themeChanger.querySelector("i").classList.toggle("fa-sun")
//        body.classList.toggle("dark");

//    });
//})();

/*********** About section tab **************/
(() => {
    const aboutSection = $(".about-section"),
        tabsContainer = $(".about-tabs");

    tabsContainer.click((event) => {
        if (event.target.classList.contains("tab-item") &&
            !event.target.classList.contains("active")) {
            const target = event.target.getAttribute("data-target");
            tabsContainer.find(".active")[0].classList.remove("outer-shadow", "active");
            event.target.classList.add("outer-shadow", "active");

            aboutSection.find(".tab-content.active")[0].classList.remove("active");
            aboutSection.find(target)[0].classList.add("active");

        }
    })
})();


function bodyStopScrollingToggle() {
    document.body.classList.toggle("stop-scrolling");
}
/*********** Portfolio popup and filter ****************/
(() => {

    const filterContainer = document.querySelector(".portfolio-filter"),
        portfolioItemsContainer = document.querySelector(".portfolio-items"),
        portfolioItems = document.querySelectorAll(".portfolio-item"),
        popup = document.querySelector(".portfolio-popup"),
        prevBtn = popup.querySelector(".pp-prev"),
        nextBtn = popup.querySelector(".pp-next"),
        closeBtn = popup.querySelector(".pp-close"),
        projectDetailsContainer = popup.querySelector(".project-details-container"),
        projectDetailsBtn = popup.querySelector(".pp-project-detail-btn");

    let itemIndex, slideIndex, screenshots;

    /*filter portfolio items*/
    filterContainer.addEventListener("click", (event) => {
        if (event.target.classList.contains("filter-item") &&
            !event.target.classList.contains("active")) {
            filterContainer.querySelector(".active").classList.remove("outer-shadow", "active");
            event.target.classList.add("outer-shadow", "active")
            const target = event.target.getAttribute("data-target");
            portfolioItems.forEach((item) => {
                if (target == "all") {
                    item.classList.remove("hide");
                    item.classList.add("show");

                }
                else {
                    if (target === item.getAttribute("data-category")) {
                        item.classList.remove("hide");
                        item.classList.add("show");
                    }
                    else {
                        item.classList.remove("show");
                        item.classList.add("hide");
                    }
                }
            })
        }
    })

    portfolioItemsContainer.addEventListener("click", (event) => {
        if (event.target.closest(".portfolio-item-inner")) {
            const portfolioItem = event.target.closest(".portfolio-item-inner").parentElement;

            itemIndex = Array.from(portfolioItem.parentElement.children).indexOf(portfolioItem);
            screenshots = portfolioItems[itemIndex].querySelector(".portfolio-item-img img").getAttribute("data-screenshots").split(",");
            console.log(screenshots)
            if (screenshots.length === 1) {
                prevBtn.style.display = "none";
                nextBtn.style.display = "none";
                if (screenshots[0] === "") {
                    projectDetailsBtn.style.display = "none";
                    popup.querySelector(".pp-counter").style.display = "none";
                }
            }

            else {
                prevBtn.style.display = "block";
                nextBtn.style.display = "block";
            }
            slideIndex = 0;
            popupToggle();
            popupSlideShow();
            popupDetails();
        }
    })

    //Handle popop title and category
    function popupDetails() {
        var item = portfolioItems[itemIndex];

        var title = item.getAttribute("data-name");
        var category = item.getAttribute("data-category");
        var description = item.getAttribute("data-description");

        var tools = item.getAttribute("data-tools").split(",");
        var github = item.getAttribute("data-github");

        popup.querySelector(".pp-title h2").innerHTML = title;
        popup.querySelector(".pp-project-category").innerHTML = category;
        popup.querySelector(".description__text").innerHTML = description;
        tools.forEach((tool) => {
             var span = document.createElement("span");
            span.classList.add("tool");
            span.innerHTML = tool + " ";
            popup.querySelector(".tools-holder").appendChild(span);
        });

        popup.querySelector(".project-link").innerHTML = title;
        popup.querySelector(".project-link").href = github;
    }

    closeBtn.addEventListener("click", () => {
        popupToggle();
        popup.querySelector(".tools-holder").innerHTML = "";
    })

    function popupToggle() {
        popup.classList.toggle("open");
        projectDetailsBtn.querySelector("i").classList.remove("fa-plus");
        projectDetailsBtn.querySelector("i").classList.add("fa-minus");
        projectDetailsContainer.classList.add("active");
        popup.scrollTo(0, projectDetailsContainer.offsetTop)
        bodyStopScrollingToggle();
    }
    function popupSlideShow() {
        const imgSrc = screenshots[slideIndex];
        const popupImg = popup.querySelector(".pp-img");
        popup.querySelector(".pp-loader").classList.add("active");

        popupImg.src = imgSrc;
        popupImg.onload = () => {
            popup.querySelector(".pp-loader").classList.remove("active");
        }

        popup.querySelector(".pp-counter").innerHTML = (slideIndex + 1) + " of " + screenshots.length;
    }

    nextBtn.addEventListener("click", () => {
        if (slideIndex === screenshots.length - 1) {
            slideIndex = 0;
        }
        else {
            slideIndex++;
        }
        popupSlideShow();
    })

    prevBtn.addEventListener("click", () => {
        if (slideIndex === 0) {
            slideIndex = screenshots.length - 1;
        }
        else {
            slideIndex--;
        }
        popupSlideShow();
    })


    projectDetailsBtn.addEventListener("click", () => {
        popupDetailsToggle();
    })

    function popupDetailsToggle() {
        if (projectDetailsContainer.classList.contains("active")) {
            projectDetailsBtn.querySelector("i").classList.remove("fa-minus");
            projectDetailsBtn.querySelector("i").classList.add("fa-plus");
            projectDetailsContainer.classList.remove("active");
        }
        else {
            projectDetailsBtn.querySelector("i").classList.remove("fa-plus");
            projectDetailsBtn.querySelector("i").classList.add("fa-minus");
            projectDetailsContainer.classList.add("active");
            popup.scrollTo(0, projectDetailsContainer.offsetTop)
        }
    }

})();


/********* Inspiration slider *********/
(() => {
    const slideContainer = document.querySelector(".inspiration-slider-container"),
        slideItems = slideContainer.querySelectorAll(".inspiration-item"),
        slideWidth = slideContainer.offsetWidth,
        prevBtn = document.querySelector(".inspiration-slider-nav .prev"),
        nextBtn = document.querySelector(".inspiration-slider-nav .next");

    let slideIndex = 0;

    /*set width of all slides */
    slideItems.forEach((slide) => {
        slide.style.width = slideWidth + "px";
    })

    /*set the container to the correct size */
    slideContainer.style.width = slideWidth * slideItems.length + "px";

    nextBtn.addEventListener("click", () => {
        if (slideIndex === slideItems.length - 1) {
            slideIndex = 0;
        }
        else {
            slideIndex++;
        }
        distance = slideWidth * slideIndex;
        slideContainer.style.marginLeft = -distance + "px";
    })

    prevBtn.addEventListener("click", () => {
        if (slideIndex === 0) {
            slideIndex = slideItems.length - 1;
        }
        else {
            slideIndex--;
        }
        distance = slideWidth * slideIndex;
        slideContainer.style.marginLeft = -distance + "px";
    })

})();


/********* section  activation *********/

//(() => {
//    const sections = document.querySelectorAll(".section");

//    sections.forEach((section) => {
//        if (!section.classList.contains("active")) {
//            section.classList.add("hide");
//        }
//    })
//})()