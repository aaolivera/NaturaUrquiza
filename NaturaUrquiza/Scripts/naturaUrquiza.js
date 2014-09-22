function Producto(id, nombre, precio, precioPromocional,cantidad) {
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
   
    selff.PrecioVisible = ko.computed(function () {
        return "$" + selff.Precio + " x " + selff.Cantidad() + " = $" + parseFloat(selff.Precio) * selff.Cantidad();
    });
}

function ProductosListViewModel() {
    // Inicializo observers
    var self = this;
    self.Productos = ko.observableArray([]);
    self.CantidadSeleccionados = ko.observable(0);
    
    // Obtengo objetos Descuento del dominio
    var productos = $.cookie("productos");
    if (productos != null) {
        var mappedDescuentos = $.map(JSON.parse(productos),
            function(item) {
                 return new Producto(item.Id, item.Nombre, item.Precio,null, item.Cantidad);
            });
        self.Productos(mappedDescuentos);
    }

    // Operations
    self.agregarProducto = function (id, nombre, precio, precioPromocional) {
        
        var match = ko.utils.arrayFirst(self.Productos(), function (item) {
            return id === item.Id;
        });

        if (!match) {
            self.Productos.push(new Producto(id, nombre, precio, precioPromocional));
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
    
}

$(document).ready(function () {
    ko.applyBindings(new ProductosListViewModel());
});
