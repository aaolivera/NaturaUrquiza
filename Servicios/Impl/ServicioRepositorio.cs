using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dominio.Consultas;
using Dominio.Dto;
using Dominio.Entidades;
using Repositorio;
using Servicios.Conversiones;

namespace Servicios.Impl
{
    public class ServicioRepositorio : IServicioRepositorio
    {
        private readonly IRepositorio repositorio;
        private readonly IConversor conversor;

        public ServicioRepositorio(IRepositorio repositorio, IConversor conversor)
        {
            this.repositorio = repositorio;
            this.conversor = conversor;
        }

        public string Saludar()
        {
            return "Hola Mundo";
        }

        public List<UsuarioDto> ListarUsuarios()
        {
            return Listar<Usuario, UsuarioDto>().ToList();
        }

        public ListaPaginada<ProductoDto> ListarProductos(string filtro, Paginacion paginacion)
        {
            return Listar<Producto, ProductoDto>(x => x.Nombre.Contains(filtro), paginacion);
        }

        public ProductoDto ObtenerProducto(int id)
        {
            return Obtener<Producto, ProductoDto>(id);
        }


        public List<LineaDto> ListarLineas()
        {
            return Listar<Linea, LineaDto>().ToList();
        }

        public List<TipoDto> ListarTipos()
        {
            return Listar<Tipo, TipoDto>().ToList();
        }













        private ListaPaginada<TDto> Listar<TEntidad, TDto>(Expression<Func<TEntidad, bool>> expresionFiltro, Paginacion paginacion) where TEntidad : class
        {
            return conversor.ConvertirListaPaginada<TEntidad, TDto>(repositorio.Listar(expresionFiltro, paginacion));
        }

        private IList<TDto> Listar<TEntidad, TDto>() where TEntidad : class
        {
            return conversor.ConvertirList<TEntidad, TDto>(repositorio.Listar<TEntidad>());
        }

        private IList<TDto> Listar<TEntidad, TDto>(Expression<Func<TEntidad, bool>> expresionFiltron) where TEntidad : class
        {
            return conversor.ConvertirList<TEntidad, TDto>(repositorio.Listar(expresionFiltron));
        }

        /// <summary>
        /// Busca una entidad por Id en el repositorio y la devuelve mapeada como DTO
        /// </summary>
        /// <typeparam name="TEntidad"></typeparam>
        /// <typeparam name="TDto"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        private TDto Obtener<TEntidad, TDto>(int id) where TEntidad : class
        {
            return conversor.Convertir<TEntidad, TDto>(repositorio.Obtener<TEntidad>(id));
        }

        private ListaPaginada<TDto> ObtenerListaPaginada<TEntidad, TDto>(Paginacion paginacion, IQueryable<TEntidad> lista)
        {
            if (paginacion.OrdenarPor != null && lista != null)
            {
                var selectorOrden = Expresiones.Propiedad<TEntidad>(paginacion.OrdenarPor);
                lista = paginacion.DireccionOrden == DirOrden.Asc
                                      ? lista.OrderBy(selectorOrden)
                                      : lista.OrderByDescending(selectorOrden);
            }
            var itemsTotales = 0;
            if (lista != null)
            {
                itemsTotales = lista.Count();
                lista =
                    lista.Skip((paginacion.Pagina - 1) * paginacion.ItemsPorPagina).Take(paginacion.ItemsPorPagina);
            }
            var listaPaginada = new ListaPaginada<TEntidad>(lista.ToList(), paginacion.Pagina,
                                                                             paginacion.ItemsPorPagina, itemsTotales);
            return conversor.ConvertirListaPaginada<TEntidad, TDto>(listaPaginada);
        }

    }
}
