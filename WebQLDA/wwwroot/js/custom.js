<script>
    $(document).ready(function () {
        // Xử lý sự kiện khi ấn vào link
        $('.cong-viec-link').click(function (e) {
            e.preventDefault();

            // Lấy danh sách tên công việc từ thuộc tính data
            var congViecList = $(this).data('cong-viec').split(',');

            // Hiển thị popup modal
            $('#cong-viec-modal .modal-body').html(congViecList.join('<br>'));

            $('#cong-viec-modal').modal('show');
        });
    });
</script>
