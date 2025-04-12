const registerWarning = $(".register-warning")

if (!registerWarning.text())
    registerWarning.hide()

$(".register-form").on("submit", event => {
    console.log("dsadasdas")
    if ($("input[name=password]").val() != $("input[name=confirmPassword]").val()) {
        event.preventDefault()
        event.stopPropagation()
        registerWarning.text("Пароли не совпадают")
        registerWarning.show()
    }
})