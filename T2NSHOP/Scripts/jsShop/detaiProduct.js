var dataId;
var quantityShow;
var selectedValues = {
    color: '',
    size: ''
};
$(document).ready(function () {
    //Quantity
    var productDetails = document.querySelector('.product-details');
    if (productDetails != null) {
        var productId = productDetails.dataset.productId;
        ShowQuantity(productId, selectedValues.color, selectedValues.size);
    }
    var quantity = parseInt($('#quantity').val());
    //  nút "-"
    $('.qty-btn[value="-"]').on('click', function () {
        if (quantity > 1) {
            quantity--;
            $('#quantity').val(quantity);
        }
    });
    //  nút "+"
    $('.qty-btn[value="+"]').on('click', function () {
        quantity++;
        if (quantity > quantityShow) {
            $(document).Toasts('create', {
                title: 'Thông báo',
                class: 'bg-danger',
                autohide: true,
                delay: 1300,
                body: 'Bạn đã chọn số lượng sản phẩm tồn kho của Shop. Vui lòng liên hệ Shop để được hỗ trợ !!!'
            });
            quantity--;
        }
        $('#quantity').val(quantity);
    });

    //image
    $(".product-gallery__thumb img").click(function () {
        $(".product-gallery__thumb").removeClass('active');
        $(this).parents('.product-gallery__thumb').addClass('active');

        $(".product-image-detail .product-image-feature").attr("src", $(this).attr("data-image"));

    });
    $(".product-gallery__thumb").first().addClass('active');
    $('input[name="option2"]').on('click', function () {
        var selectedColor = $(this).val();
        $('.product-gallery__thumb').each(function () {
            var $thumb = $(this);
            var thumbColor = $thumb.find('img').attr('data-color');
            var thumbimage = $thumb.find('img').attr('data-image');
            if (thumbColor === selectedColor) {
                $(".product-gallery__thumb").removeClass('active');
                $(this).addClass('active');
                $(".product-image-detail .product-image-feature").attr("src", thumbimage);
            }
        });
    });
    $('body').on('click', '#add-item-form .swatch input', function (e) {
        e.preventDefault();
        var $this = $(this);
        $this.parent().siblings().find('label').removeClass('sd');
        $this.next().addClass('sd');
    });

    //color
    $('input[name="option2"]').on('click', function () {
        var color = $(this).val();
        var id = $(this).data('id');
        document.getElementById('spancolor').textContent = color;
        selectedValues.color = color;
        ShowQuantity(productId, selectedValues.color, selectedValues.size);

        $.ajax({
            url: '/products/GetSizesByColor',
            type: 'POST',
            data: { color: color, id: id },
            dataType: 'json',
            success: function (response) {
                var sizes = response.Sizes;
                $('.size-quantity').hide();
                for (var i = 0; i < sizes.length; i++) {
                    var size = sizes[i];
                    $('.size-quantity[data-size="' + size + '"]').show();
                }
            }
        });
        quantity = 1;
        $('#quantity').val(quantity);
    });

    //size
    $('input[name="option1"]').on('click', function () {
        var size = $(this).val();
        document.getElementById('spansize').textContent = size;
        selectedValues.size = size;
        ShowQuantity(productId, selectedValues.color, selectedValues.size);
        quantity = 1;
        $('#quantity').val(quantity);
    });

    // thanh toán
    $('a.btn-success').on('click', function (event) {
        event.preventDefault(); // Ngăn chặn chuyển hướng đến trang thanh toán mặc định của nút <a>
        CheckShoppingCartNotEmpty();
    });

    //đăng nhập để mua hàng
    $('body').on('click', '.btnregister', function () {
        $('#modal-registerrhelp').modal('show');
    });

    // addToCart
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var headers = {};
        var token = $('input[name="__RequestVerificationToken"]').val();
        headers['__RequestVerificationToken'] = token;
        var id = $(this).data('id');
        var tquantity = 1;
        var Quantity = parseInt($('#quantity').val());;
        if (Quantity != '') {
            tquantity = Quantity;
        }
        var color = selectedValues.color;
        var size = selectedValues.size;
        if (checkSelectedValues()) {
            $.ajax({
                url: '/shoppingcart/addtocart',
                type: 'POST',
                headers: headers,
                data: { id: id, quantity: tquantity, color: color, size: size },
                success: function (rs) {
                    if (rs.Success) {
                        $('#checkout_items').html(rs.Count);

                        $(document).Toasts('create', {
                            title: 'Thông báo',
                            class: 'bg-success',
                            autohide: true,
                            delay: 750,
                            body: 'Thêm sản phẩm vào giỏ hàng thành công.'
                        });
                    }
                    else {
                        $(document).Toasts('create', {
                            title: 'Thông báo',
                            class: 'bg-danger',
                            autohide: true,
                            delay: 750,
                            body: 'Thêm sản phẩm vào giỏ hàng thất bại. Xin vui lòng thử lại!!!'
                        });
                    }
                }
            });
        }
        else {
            $(document).Toasts('create', {
                title: 'Thông báo',
                class: 'bg-danger',
                autohide: true,
                delay: 750,
                body: ' Chọn các tùy chọn cho sản phẩm trước khi cho sản phẩm vào giỏ hàng của bạn.!!!'
            });
        }

        /* alert(id + " " + quantity);*/

    });
});

function ShowQuantity(id, color, size) {
    var headers = {};
    var token = $('input[name="__RequestVerificationToken"]').val();
    headers['__RequestVerificationToken'] = token;
    $.ajax({
        url: '/products/ShowQuantity',
        type: 'POST',
        headers: headers,
        data: { id: id, color: color, size: size },
        dataType: 'json',
        success: function (rs) {
            quantityShow = rs.quantity;
            $('#showquantitydata').html(quantityShow);
        }
    });
}
//kiểm tra giá trị cho color và size. nếu false thì không được mua
function checkSelectedValues() {
    if (selectedValues.color === '' || selectedValues.size === '') {
        return false;
    }
    return true;
}