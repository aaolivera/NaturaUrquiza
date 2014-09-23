function Producto(id, nombre, precio, precioPromocional, refrescar, cantidad) {
    //id trae el objeto que ya existia
    var selff = this;
    selff.Id = id;
    selff.Nombre = nombre;
    
    if (cantidad != null) {
        selff.Cantidad = ko.observable(cantidad);
    } else {
        selff.Cantidad = ko.observable(1);
    }

    selff.Precio = precio;
    if (precioPromocional != null) {
        selff.Precio = precioPromocional;
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

function ProductosListViewModel() {
    // Inicializo observers
    var self = this;
    self.Productos = ko.observableArray([]);
    self.CantidadSeleccionados = ko.observable(0);
    self.Refrescar = ko.observable();
    
    // Obtengo objetos Descuento del dominio
    var productos = $.cookie("productos");
    if (productos != null) {
        var mappedDescuentos = $.map(JSON.parse(productos),
            function(item) {
                return new Producto(item.Id, item.Nombre, item.Precio,null, self.Refrescar, item.Cantidad);
            });
        self.Productos(mappedDescuentos);
    }

    // Operations
    self.agregarProducto = function (id, nombre, precio, precioPromocional) {
        
        var match = ko.utils.arrayFirst(self.Productos(), function (item) {
            return id === item.Id;
        });

        if (!match) {
            self.Productos.push(new Producto(id, nombre, precio, precioPromocional, self.Refrescar));
        } else {
            match.Cantidad(match.Cantidad() + 1);
        }
        $.cookie("productos", ko.toJSON(self.Productos));
    };
    
    self.cantidadDeProducto = function (data) {
        var match = ko.utils.arrayFirst(self.Productos(), function (item) {
            return item.Id === data;
        });
        return match.Cantidad();
    };

    self.refrescarFunc = function() {
        self.Refrescar.notifySubscribers();
    };
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
});
