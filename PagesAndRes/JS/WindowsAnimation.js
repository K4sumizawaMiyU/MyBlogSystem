function fadeIn(element, speed) {
    var speed = speed || 30;
    var num = 0;
    element.style.opacity = 0;
    element.style.display = 'block';
    var st = setInterval(function () {
        num++;
        element.style.opacity = num / 10;
        if (num >= 10) {
            clearInterval(st);
        }
    }, speed);
}

function fadeOut(element, speed) {
    var speed = speed || 30;
    var num = 10;
    var st = setInterval(function () {
        num--;
        element.style.opacity = num / 10;
        if (num <= 0) {
            clearInterval(st);
            element.style.display = 'none';
        }
    }, speed);
}

function BackGroundfadeIn(element, speed) {
    var speed = speed || 30;
    var num = 0;
    element.style.opacity = 0;
    element.style.display = 'flex';
    var st = setInterval(function () {
        num++;
        element.style.opacity = num / 10;
        if (num >= 10) {
            clearInterval(st);
        }
    }, speed);
}

document.getElementById('TestButton').onclick = function () {
    fadeOut(LoginButton, 20);
    setTimeout(function () {
        BackGroundfadeIn(overlay, 20);
        fadeIn(login,20);
    }, 200);
}

document.getElementById('toRegister').onclick = function () {
    fadeOut(login, 20);
    setTimeout(function () {
        fadeIn(register, 20);
    }, 200);
}

document.getElementById('toLogin').onclick = function () {
    fadeOut(register, 20);
    setTimeout(function () {
        fadeIn(login, 20);
    }, 200);
}
document.getElementById('overlay').addEventListener('click', function(event) {
    var login = document.getElementById('login');
    var register = document.getElementById('register');

    if (!login.contains(event.target) && !register.contains(event.target)) {
        fadeOut(overlay,20);
        fadeOut(login,20);
        fadeOut(register,20);
        setTimeout(function () {
            fadeIn(LoginButton, 20);
        }, 200);
    }
});