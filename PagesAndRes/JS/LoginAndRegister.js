document.getElementById('submit').addEventListener('click', function(event) {
    event.preventDefault();
    axios({
        method: "POST",
        url: `http://localhost:6060/api/Authorize/Login?username=${username.value}&password=${CryptoJS.MD5(password.value).toString()}`
    }).then(response => {
            console.log(response.data);
            if (response.data.code == 200) {
                alert("登录成功");
                fadeOut(login, 20);
                fadeOut(overlay, 20);
                setTimeout(function () {
                    fadeIn(TestLogOut, 20);
                }, 200);
                TestLogOut.value = response.data.data["name"].charAt(0);
            } else {
                alert("登录失败");
            }
        })
        .catch(error => {
            console.error('Error:', error);
        });
});
document.getElementById('reg_submit').addEventListener('click', function(event) {
    event.preventDefault();
    if (reg_password.value != reg_cf_password.value) {
        alert("两次输入的密码不一致(*￣3￣)╭");
    } else {
        axios({
            method: "POST",
            url: `http://localhost:5000/api/AuthorInfo/CreateUser?name=${reg_name.value}&username=${reg_username.value}&pwd=${reg_password.value}`,
        }).then(response => {
            console.log(response.data);
            if (response.data.code == 200) {
                alert("注册成功！");
                fadeOut(register, 20);
                setTimeout(function () {
                    fadeIn(login, 20);
                }, 200);
            } else {
                alert("注册失败！");
            }
        })
            .catch(error => {
                console.error('Error:', error);
            });
    }
});