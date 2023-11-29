window.onload = function () {
    loadPage();
}
function loadPage() {
    axios.get('http://localhost:5000/api/BlogNews/BlogNewsPage?page=1&size=5')
        .then(response => {
            if (response.data.code == 200) {
                let itemBox= document.querySelector('.item')
                let html = ""
                let i = 1;
                for (let article of response.data.data) {
                    if (article.authorName == null) {
                        html += `<div class="item-box  layer-photos-demo1 layer-photos-demo" id=${i}><h3><a href="">${article.title}</a></h3><h5>作者: <span>佚名</span></h5><p>${article.content}</p></div>`;
                    } else {
                        html += `<div class="item-box  layer-photos-demo1 layer-photos-demo" id=${i}><h3><a href="">${article.title}</a></h3><h5>作者: <span>${article.authorName}</span></h5><p>${article.content}</p></div>`;
                    }
                    i ++;
                }
                itemBox.innerHTML = html;
            }
            else
            {
                alert("加载失败");
            }
        })
}