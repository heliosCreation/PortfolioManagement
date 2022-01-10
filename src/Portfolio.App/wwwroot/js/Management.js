(() => {
    const selectDropDown = document.querySelector(".topic-choice select");
    const allTopic = document.querySelectorAll(".sidebar-nav .topic");

    const sideBarBtns = document.querySelectorAll(".sidebar-nav li a");



    var options = document.querySelectorAll(".custom-option");

    selectDropDown.addEventListener("change", function () {
        let active = document.querySelectorAll(".sidebar-nav .topic.active");
        let selectedValue = selectDropDown.options[selectDropDown.selectedIndex];
        let targetsClass = selectedValue.getAttribute("data-target");
        if (targetsClass === "all-topic") {
            allTopic.forEach((topic) => {
                topic.classList.add("active");
            })
        }
        else {
            let toActivate = document.querySelectorAll("." + targetsClass);

            active.forEach((node) => {
                node.classList.remove("active");
            })
            toActivate.forEach((node) => {
                node.classList.add("active");
            })
        }

    })

    
})()


function setSideNav(nav) {
    const sideBarDownPart = ["Inspiration", "Projects", "Contact", "Project-category"];

    $(".sidebar-nav li a.active").removeClass("active");
    var sidebarTarget = $(".sidebar-nav li a[data-target='" + nav + "']")[0];
    sidebarTarget.classList.add("active");
    console.log(sideBarDownPart.indexOf(nav) < 0)
    if (sideBarDownPart.indexOf(nav) < 0 === false) {
        var offTop = $(sidebarTarget).offset().top;
        $("#sidebar-wrapper").scrollTop(offTop);
    }
};

function AttachModalCreateListener(val) {
    $("#management-add-btn").on('click', function () {
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: val,
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (result) {
                $("#management-modal").modal("show")
                $(' #management-modal .modal-content').html(result);
            },
            error: function (data) {
                alert("Error loading dynamic data");
            }
        });
    });
}

function AttachTableModalListeners(buttons, url) {
    buttons.forEach((btn) => {
        $(btn).on('click', () => {
            let parentContainer = btn.closest("tr");
            let targetId = parentContainer.querySelector(".d-none").innerText;
            $.ajax({
                type: "GET",
                url: url,
                contentType: "application/json; charset=utf-8",
                data: { Id: targetId },
                datatype: "json",
                success: function (result) {
                    $("#management-modal").modal("show")
                    $('#management-modal .modal-content').html(result);
                },
                error: function (data) {
                    alert("Error loading dynamic data");
                    console.log(data)
                }
            });
        })
    })
}

function setCoverListeners() {
    var imageInput = document.getElementById("cover-input");
    var imageBtn = document.getElementById("cover-btn");
    var imagePreview = document.getElementById("image-preview");
    var imagePreviewImg = document.querySelector(".image-preview__image");
    var imagePreviewText = document.querySelector(".image-preview__text");


    imageBtn.addEventListener("click", () => {
        imageInput.click();
    });
    imageInput.addEventListener("change", () => {
        const file = imageInput.files[0];

        if (file) {
            // Make sure `file.name` matches our extensions criteria

            if (!/\.(jpe?g|png)$/i.test(file.name)) {
                return alert(file.name + " is not an image");
            }
            const reader = new FileReader();

            imagePreview.style.border = "none";
            imagePreviewText.style.display = "none";
            imagePreviewImg.style.display = "block";

            reader.onload = function (e) {
                imagePreviewImg.setAttribute("src", e.target.result.toString());
            }
            reader.readAsDataURL(file);

        }
    });
}


