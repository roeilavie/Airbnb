function animation(div) {
    div.classList.add("animate");

    setTimeout(function () {
        div.classList.remove("animate");
    }, 500); // 500 is the same time as the CSS animation
}