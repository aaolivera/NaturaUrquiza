function Producto(id, nombre, precio, precioPromocional, urlImage, refrescar, codigo, cantidad) {
    //id trae el objeto que ya existia
    var selff = this;
    selff.Id = id;
    selff.Nombre = nombre;
    selff.Codigo = codigo;
    selff.UrlImage = urlImage;
    if (cantidad != null) {
        selff.Cantidad = ko.observable(cantidad);
    } else {
        selff.Cantidad = ko.observable(1);
    }

    selff.Precio = parseFloat(precio);
    if (precioPromocional != null) {
        selff.Precio = parseFloat(precioPromocional);
    }
    selff.Refrescar = refrescar;
    selff.PrecioVisible = ko.computed(function () {
        selff.Refrescar();
        if (selff.Cantidad() > 0) {
            $("#" + selff.Id + "-jewel").removeClass('hide');
            $("#" + selff.Id + "-jewel").html(selff.Cantidad());
        } else {
            $("#" + selff.Id + "-jewel").addClass('hide');
        }
        return "$" + selff.Precio + " x " + selff.Cantidad() + " = $" + parseFloat(selff.Precio) * selff.Cantidad();
    });
}

function Total(productos, refrescar) {
    //id trae el objeto que ya existia
    var selff = this;
    selff.Nombre = "Total: ";
    selff.Productos = productos;
    selff.Refrescar = refrescar;
    selff.PrecioVisible = ko.computed(function () {
        selff.Refrescar();
        var total = 0;
        ko.utils.arrayForEach(selff.Productos(), function (producto) {
            total += parseFloat(producto.Precio) * producto.Cantidad();
        });
        return "$" + total;
    });
}

function ProductosListViewModel() {
    // Inicializo observers
    var self = this;
    self.Productos = ko.observableArray([]);
    self.Refrescar = ko.observable();
    self.Total = new Total(self.Productos, self.Refrescar);
    // Obtengo objetos Descuento del dominio
    var productos = $.cookie("productos");
    if (productos != null) {
        var mappedDescuentos = $.map(JSON.parse(productos),
            function(item) {
                return new Producto(item.Id, item.Nombre, item.Precio, null, item.UrlImage, self.Refrescar, item.Codigo, item.Cantidad);
            });
        self.Productos(mappedDescuentos);
    }

    // Operations
    self.agregarProducto = function (id, nombre, precio, precioPromocional, urlImage, codigo) {
        
        var match = ko.utils.arrayFirst(self.Productos(), function (item) {
            return id === item.Id;
        });

        if (!match) {
            self.Productos.push(new Producto(id, nombre, precio, precioPromocional, urlImage, self.Refrescar, codigo));
        } else {
            var a = match.Cantidad();
            match.Cantidad(a + 1);
        }
        $.cookie("productos", ko.toJSON(self.Productos));
    };
    
    self.quitarProducto = function (value) {
        var match = self.Productos.remove(value);
        if (match.length > 0) {
            value.Cantidad(0);
            value._destroy = true;
            $.cookie("productos", ko.toJSON(self.Productos));
        }
    };
    
    self.cantidadDeProducto = ko.computed(function () {
        var total = 0;
        ko.utils.arrayForEach(self.Productos(), function (feature) {
            total += feature.Cantidad();
        });
        return total;
    });

    self.refrescarFunc = function() {
        self.Refrescar.notifySubscribers();
    };

    $("#ComprasDropDownButton").on("click", function() {
        if (self.Productos().length > 0) {
            return true;
        } else {
            return false;
        }
    });
}

$(document).ready(function () {
    var modelProductos = new ProductosListViewModel();
    ko.applyBindings(modelProductos);
    
    $(document).on('click', '#icongridFoot ul li a', function (evt) {
        var container = $(this).parents('#gridContainer');
        if (container.attr('data-grid-url')) {
            container.data().gridUrl = this.href;
        }
        $.get(this.href, function (data) {
            var element = $('#gridContainer')[0];
            ko.cleanNode(element);
            
            container.html(data);
            ko.applyBindings(modelProductos, document.getElementById("gridContainer"));
            modelProductos.refrescarFunc();
        });
        return false;
    });

    $(".ComprasList").on("click", function () {
        return false;
    });
    
    $('#ComprasList').click(function (e) {
        e.stopPropagation();
    });

    $("#boton-comprar").on('click', function() {
        $('#dialogo-comprar').modal().css({
            'top': '5%',
        });;
        $("#dialogo-comprar-comprar").attr("disabled", false);
    });
});
