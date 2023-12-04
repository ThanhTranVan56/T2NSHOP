var dataId;
var isChecked = {};
$(document).ready(function () {
    GetTotalPriceIsActive()

    isChecked = JSON.parse(localStorage.getItem('isChecked')) || {};

    // Lấy danh sách cáccheckbox trong bảng
    var checkboxes = $('table.tableItem tbody tr td input[type="checkbox"]');
    var headers = {};
    var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
    headers['__RequestVerificationToken'] = token;
    // Đặt lại trạng thái của các checkbox bằng cách sử dụng biến isChecked
    checkboxes.each(function () {
        var id = $(this).val();
        if (isChecked.hasOwnProperty(id)) {
            $(this).prop('checked', isChecked[id]);
            UpdateIsActive(id, isChecked[id], headers);
        }
    });

    //shoppingcartGetSizeByColor
    $('body').on('click', '.btnclickcolor', function () {
        var headers = {};
        var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var color = $(this).val();
        $.ajax({
            url: '/shoppingcart/GetSizesByColor',
            type: 'POST',
            headers: headers,
            data: { color: color },
            dataType: 'json',
            success: function (response) {
                var sizes = response.Sizes;
                $('.btnsize').hide();
                for (var i = 0; i < sizes.length; i++) {
                    var size = sizes[i];
                    $('.btnsize[data-size="' + size + '"]').show();
                }
            }
        });
    });

    $('body').on('click', '#btnButton', function () {
        dataId = $(this).data('id');
    });
    $('body').on('click', '.btnSuccesscolorsize', function () {
        var headers = {};
        var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var colorv = $('.btnclickcolor.active').data('color');
        var sizev = $('.btnsize.active').data('size');
        var id = dataId;
        $.ajax({
            url: '/ShoppingCart/UpdateColorSize',
            type: 'POST',
            headers: headers,
            data: { id: id, color: colorv, size: sizev },
            success: function (rs) {
                if (rs.Success) {
                    LoadCart();
                }
            }
        });
    });
    // Lưu trữ thông tin về button color đã click trước đó
    var prevclBtn = null;
    // Sự kiện click vào button color
    $('.btnclickcolor').click(function () {
        // Đổi màu nền của button
        if (prevclBtn != null) {
            prevclBtn.removeClass('active');
        }
        prevclBtn = $(this);
        $(this).addClass('active');
    });
    // Lưu trữ thông tin về button color đã click trước đó
    var prevszBtn = null;
    // Sự kiện click vào button size
    $('.btnsize').click(function () {
        // Cập nhật giá trị size vào view
        if (prevszBtn != null) {
            prevszBtn.removeClass('active');
        }
        prevszBtn = $(this);
        $(this).addClass('active');
    });

    $('#selectAll').change(function () {
        var headers = {};
        var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var isChecked = $(this).is(':checked');
        // Đặt trạng thái của tất cả các checkbox ở thứ tự thứ hai là giống với checkbox ở thứ tự đầu tiên
        $('table.tableItem tbody tr td input[type="checkbox"]').prop('checked', isChecked);
        //Cập nhật IsAcive ở data
        if (this.checked) {
            console.log('Tất cả Checkbox được chọn');
            UpdateIsActiveAll(this.checked, headers);
        } else {
            console.log('Tất cả Checkbox bị bỏ chọn');
            UpdateIsActiveAll(this.checked, headers);
        }
    });

    // Lắng nghe sự kiện khi người dùng tick vào checkbox ở thứ tự thứ hai trở đi
    $('table.tableItem tbody').on('change', 'tr td input[type="checkbox"]', function () {
        debugger;
        var headers = {};
        var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        // Cập nhật số lượng sản phẩm được chọn ở #checkoutcart
        var id = $(this).val();
        isChecked[id] = this.checked;
        if (this.checked) {
            console.log('Checkbox có giá trị ' + id + ' được chọn');
            UpdateIsActive(id, this.checked, headers);
        } else {
            console.log('Checkbox có giá trị ' + id + ' bị bỏ chọn');
            UpdateIsActive(id, this.checked, headers);
        }
        localStorage.setItem('isChecked', JSON.stringify(isChecked));
    });


    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var headers = {};
        var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var id = $(this).data('id');
        $(document).Toasts('create', {
            title: 'Thông báo',
            class: 'bg-warning',
            autohide: true,
            delay: 750,
            body: 'Đang tiến hành xóa sản phẩm khỏi giỏ hàng !!! '
        });
        $.ajax({
            url: '/shoppingcart/Delete',
            type: 'POST',
            headers: headers,
            data: { id: id },
            success: function (rs) {
                if (rs.Success) {
                    $('#checkout_items').html(rs.count);
                    $('#trow_' + id).remove();
                    location.reload();
                }
            }
        });
    });
    $('body').on('change', '#selectAll', function () {
        var checkStatus = this.checked;
        var checkbox = $(this).parents('.container').find('tr th input:checkbox, tr td input:checkbox');
        checkbox.each(function () {
            this.checked = checkStatus;
            if (this.checked) {
                checkbox.attr('selected', 'checked');
            } else {
                checkbox.attr('selected', '');
            }
        });
    });
    $('body').on('click', '.btnDeleteAllCart', function (e) {
        e.preventDefault();
        var str = "";
        var headers = {};
        var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var checkbox = $(this).parents('.container').find('tr td input:checkbox');
        var i = 0;
        checkbox.each(function () {
            if (this.checked) {
                checkbox.attr('selected', 'checked');
                var _id = $(this).val();
                if (i === 0) {
                    str += _id;
                }
                else {
                    str += "," + _id;
                }
                i++;
            } else {
                checkbox.attr('selected', '');
            }
        });

        if (str.length > 0) {
            var conf = confirm('Bạn có muốn xóa các bản ghi này hay không?');
            if (conf === true) {
                $.ajax({
                    url: '/ShoppingCart/DeleteAll',
                    type: 'POST',
                    headers: headers,
                    data: { ids: str },
                    success: function (rs) {
                        if (rs.Success) {
                            $('#checkoutcart').text("0");
                            window.location.reload();

                        }
                    }
                });
            }
        }
    });

    $('body').on('click', '.qty-minus', function (e) {
        e.preventDefault();
        debugger;
        var id = $(this).data('id');
        var quantity = parseInt($('#quantity_' + id).val());
        if (quantity > 1) {
            quantity--;
            $('#quantity_' + id).val(quantity);
        }
    });
    $('body').on('click', '.qty-plus', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = parseInt($('#quantity_' + id).val());
        quantity++;
        $('#quantity_' + id).val(quantity);
    });
    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var headers = {};
        var token = $('#btnEditdbOut input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var id = $(this).data('id');
        var quantity = parseInt($('#quantity_' + id).val());
        Update(id, quantity, headers);
    });
    $('body').on('click', '#btnButton', function (e) {
        e.preventDefault();
        $('.arrow-container #arrow-box.show').removeClass('show');
        $(this).siblings('#arrow-box').toggleClass('show');
    });
    $('body').on('click', '.btn-closes', function (e) {
        e.preventDefault();
        $(this).closest('#arrow-box').removeClass('show');
    });
});

function FormatNumber(num, decimalPlaces) {
    var formatter = new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND',
        minimumFractionDigits: decimalPlaces
    });
    return formatter.format(num);
}
function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/DeleteAll',
        type: 'POST',
        headers: headers,
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
                ShowCount();
            }
        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
            /*ShowCount();*/
        }
    });
}
function Update(id, quantity, headers) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        headers: headers,
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}
function UpdateIsActive(id, isactive, headers) {
    $.ajax({
        url: '/shoppingcart/UpdateIsActive',
        type: 'POST',
        headers: headers,
        data: { id: id, isactive: isactive },
        success: function (rs) {
            if (rs.Success) {
                if (rs.isactive == true) {
                    GetTotalPriceIsActive();
                    console.log('giá trị ' + id + ' được chọn');
                }
                else {
                    GetTotalPriceIsActive();
                    console.log('giá trị ' + id + ' bị bỏ chọn');
                }

            }
        }
    });
}
function UpdateIsActiveAll(isactive, headers) {
    $.ajax({
        url: '/shoppingcart/UpdateIsActiveAll',
        type: 'POST',
        headers: headers,
        data: { isactive: isactive },
        success: function (rs) {
            if (rs.Success) {
                if (rs.isactive == true) {
                    GetTotalPriceIsActive();
                    console.log('tất cả giá trị được chọn');
                }
                else {
                    GetTotalPriceIsActive();
                    console.log('tất cả giá trị bị bỏ chọn');
                }

            }
        }
    });
}
function CheckShoppingCartNotEmpty() {
    $.ajax({
        url: 'shoppingcart/CheckShoppingCartNotEmpty',
        type: 'GET',
        success: function (rs) {
            if (rs.Success) {
                window.location.href = '/thanh-toan';
            } else {
                $(document).Toasts('create', {
                    title: 'Thông báo',
                    class: 'bg-warning',
                    autohide: true,
                    delay: 1200,
                    body: ' Bạn chưa chọn sản phẩm. Vui lòng chọn sản phẩm để thanh toán !!!'
                });
                console.log('Bạn chưa chọn sản phẩm. Vui lòng chọn sản phẩm để thanh toán !!!');
            }
        }
    });
}
function GetTotalPriceIsActive() {
    $.ajax({
        url: '/shoppingcart/GetTotalPriceIsActive',
        type: 'GET',
        success: function (rs) {
            if (rs.Count != 0) {
                $('#checkoutcartMsg').text(rs.Count + ' sản phẩm được chọn');
                $('#checkoutcartTotal').text(FormatNumber(rs.totalPrice, 0));
            }
            else {
                $('#checkoutcartMsg').text(' Khôngcó sản phẩm được chọn.');
                $('#checkoutcartTotal').text(0);
            }
        }
    });
}