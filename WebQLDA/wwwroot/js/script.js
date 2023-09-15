// Lấy modal và nút mở modal
var modal = document.getElementById('myModal');
var openModalBtn = document.getElementById('openModalBtn');

// Lấy nút đóng modal
var closeModalBtn = document.getElementById('closeModalBtn');

// Khi người dùng nhấn nút mở modal, hiển thị modal
openModalBtn.onclick = function () {
    modal.style.display = 'block';
}

// Khi người dùng nhấn nút đóng modal, ẩn modal
closeModalBtn.onclick = function () {
    modal.style.display = 'none';
}

// Khi người dùng nhấn ra ngoài modal, ẩn modal
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = 'none';
    }
}
