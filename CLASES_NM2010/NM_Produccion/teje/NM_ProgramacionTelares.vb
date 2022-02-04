Imports NM.AccesoDatos
Imports System.Data.SqlClient

Namespace NM_Tejeduria
    Public Class NM_ProgramacionTelares
        Public codigoArticulo As String
        Public codigoPlanta As String
        Public codigoTipoMaquina As String
        Public codigoTipoNuevo As String
        Public revisionArticulo As Integer
        Public cantidad As Double
        Public dias As Byte
        Public metrosTejer As Double
        Public Velocidad_Teorica As Double
        Public usuario As String
        Public fecha As Date
        Public programafecha As String
        Public programarevision As Integer
        Public numeroTelas As Short
        Public telaresreales As Double
        Public metrosreales As Double
        Public inicio As String
        Public meta As Integer
        Public observacion As String

#Region "Programa de telares - actual"
        '<><><><><><><><><><> ---Inicio: Nuevo modelo para el programa de telares--- <><><><><><><><><><>
        'Autor(s): Alexander Torres Cardenas, Luis Alanoca
        'Fecha: Octubre 2014

        ' consulta ultimo programa
        Public Sub fnc_programaciontejeduria_Ultimo(ByRef dtbProgramaTelaresFecha As DataTable)
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dtbProgramaTelaresFecha = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ultimoprograma")
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ' consulta versiones por fecha, muestra todos los programas
        Public Function fnc_programaciontejeduria_ConsultaVersiones(ByVal pstr_fechaprograma As String) As DataTable
            Dim ldtbVersiones As DataTable
            ldtbVersiones = Nothing
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_fechaprograma}
                ldtbVersiones = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_consultaversiones", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbVersiones = Nothing
            End Try
            Return ldtbVersiones
        End Function

        ' consulta cabecera de programa de telares
        Public Sub fnc_programaciontejeduriacab_listar(ByVal pstr_fechaprograma As String, ByVal pint_versionprograma As Integer, _
                                                       ByVal pstrPlanta As String, ByVal pstrTelar As String, _
                                                       ByRef dtbProgramaTelaresCab As DataTable)
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_fechaprograma, "pint_VersionPrograma", pint_versionprograma, _
                                                  "pvch_Planta", pstrPlanta, "pvch_TipoTelar", pstrTelar}
                dtbProgramaTelaresCab = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelarescabecera_listar", lobjparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ' consulta detalle de programa de telares
        Public Sub fnc_programaciontejeduriadet_listar(ByVal pstr_fechaprograma As String, _
                                                             ByVal pint_versionprograma As Integer, _
                                                             ByVal pstrPlanta As String, _
                                                             ByVal strTipoTelar As String, _
                                                             ByRef dtbProgramaTelaresDet As DataTable)
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_fechaprograma, "pint_VersionPrograma", pint_versionprograma, _
                                                  "pvch_Planta", pstrPlanta, "pvch_TipoTelar", strTipoTelar}
                dtbProgramaTelaresDet = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelaresdetalle_listar", lobjparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        ' elimina articulo del detalle del programa.
        Public Function fnc_programaciontejeduriadet_eliminar() As String
            Dim strResultado As String = ""
            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"pdtm_FechaPrograma", programafecha, _
                                                 "pint_VersionPrograma", programarevision, _
                                                 "pvch_CodigoPlanta", codigoPlanta, _
                                                 "pvch_CodigoTipoMaquina", codigoTipoMaquina, _
                                                 "pvch_CodigoArticulo", codigoArticulo, _
                                                 "pint_RevisionArticulo", revisionArticulo}
                strResultado = accesoDatos.ObtenerValor("usp_tel_programatelaresdetalle_eliminar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return strResultado
        End Function

        ' inserta articulo del detalle del programa.
        Public Function fnc_programaciontejeduriadet_insertar() As String
            Dim strResultado As String = ""
            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"pdtm_FechaPrograma", programafecha, _
                                                 "pint_VersionPrograma", programarevision, _
                                                 "pvch_CodigoPlanta", codigoPlanta, _
                                                 "pvch_CodigoTipoMaquina", codigoTipoMaquina, _
                                                 "pvch_CodigoArticulo", codigoArticulo, _
                                                 "pint_RevisionArticulo", revisionArticulo, _
                                                 "pint_NumeroTelas", numeroTelas, _
                                                 "pnum_VelocidadTeorica", Velocidad_Teorica, _
                                                 "pint_TelaresProgramados", cantidad, _
                                                 "pnum_MetrosProgramados", metrosTejer, _
                                                 "pint_DiasTrabajados", dias, _
                                                 "pint_TelaresReales", telaresreales, _
                                                 "pnum_MetrosReales", metrosreales, _
                                                 "pvch_Inicio", "", _
                                                 "pint_Meta", "", _
                                                 "pvch_Observaciones", "", _
                                                 "pvch_UsuarioCreacion", usuario}
                strResultado = accesoDatos.ObtenerValor("usp_tel_programatelaresdetalle_insertar", objParametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return strResultado
        End Function

        ' actualizar articulo del detalle del programa.
        Public Function fnc_programaciontejeduriadet_actualizar() As String
            Dim strResultado As String = ""
            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objparametros As Object() = {"pdtm_FechaPrograma", programafecha, _
                                                 "pint_VersionPrograma", programarevision, _
                                                 "pvch_CodigoPlanta", codigoPlanta, _
                                                 "pvch_CodigoTipoMaquina", codigoTipoMaquina, _
                                                 "pvch_CodigoArticulo", codigoArticulo, _
                                                 "pint_RevisionArticulo", revisionArticulo, _
                                                 "pint_NumeroTelas", numeroTelas, _
                                                 "pnum_VelocidadTeorica", Velocidad_Teorica, _
                                                 "pint_TelaresProgramados", cantidad, _
                                                 "pnum_MetrosProgramados", metrosTejer, _
                                                 "pint_DiasTrabajados", dias, _
                                                 "pint_TelaresReales", telaresreales, _
                                                 "pnum_MetrosReales", metrosreales, _
                                                 "pvch_Inicio", inicio, _
                                                 "pint_Meta", meta, _
                                                 "pvch_Observaciones", observacion, _
                                                 "pvch_UsuarioModificacion", usuario}
                strResultado = accesoDatos.ObtenerValor("usp_tel_programatelaresdetalle_actualizar", objparametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return strResultado
        End Function

        ' copia programa de telares
        Public Function fnc_programaciontejeduria_copiar(ByVal pstr_fechaprograma As String, _
                                                             ByVal pint_versionprograma As Integer, _
                                                             ByVal pstrPlanta As String, _
                                                             ByVal pstrUsuario As String) As DataTable
            Dim dtbResultado As DataTable
            dtbResultado = Nothing
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_fechaprograma, _
                                                  "pint_VersionPrograma", pint_versionprograma, _
                                                  "pvch_CodigoPlanta", pstrPlanta, _
                                                  "pvch_Usuario", pstrUsuario}
                dtbResultado = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelaresdetalle_copiar", lobjparametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return dtbResultado
        End Function

        ' nuevo programa (en blanco)
        Public Function fnc_programaciontejeduria_nuevo(ByVal pstrUsuario As String) As String
            Dim strResultado As String = ""
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"pvch_Usuario", pstrUsuario}
                strResultado = lobjaccesoDatos.ObtenerValor("usp_tel_programatelaresdetalle_nuevo", lobjparametros)
            Catch ex As Exception
                Throw ex
            End Try
            Return strResultado
        End Function

        ' consulta programas por rango de fechas
        Public Function fnc_programaciontejeduria_ConsultarProgramas(ByVal pstr_FechaDesde As String, ByVal pstr_FechaHasta As String, ByVal pstr_Estado As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_Fecha_Desde", pstr_FechaDesde,
                                                  "pvch_Fecha_Hasta", pstr_FechaHasta,
                                                  "vch_Estado", pstr_Estado}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_listaestado", lobjparametros)
            Catch ex As Exception
                ldtbProgramas = Nothing
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' actualiza el estado del programa
        Public Function fnc_programaciontejeduria_ModificaEstadoPrograma(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pstr_Estado As String) As String
            Dim strResultado As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pchr_Estado", pstr_Estado
                                                 }
                strResultado = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_actualizaestado", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return strResultado
        End Function

        ' importamos metros acabados
        Public Function fnc_programaciontejeduria_ImportarMetrosAcabados(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_ImportarMtsAcabados", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try

            Return lstrResult
        End Function

        ' consulta metros acabados
        Public Function fnc_programaciontejeduria_ConsultarMetrosAcabados(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldtbProgramas As DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_ListaMtsAcabados", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' determinacion de articulos a 30 digitos y vigencia
        Public Function fnc_programaciontejeduria_ImportarArticulosAcabados(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, _
                                                                            ByVal pvch_Usuario As String, ByVal pint_Metros As Integer) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Usuario", pvch_Usuario,
                                                  "pint_Metros", pint_Metros}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_DeterminarArticulos", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        ' valorizacion de programa de telares
        Public Function fnc_programaciontejeduria_ImportarValorizacionFinal(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Usuario", pvch_Usuario}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_Valorizacion_Importar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function
        'CAMBIO DG - NUEVA VERSION VALORIZACION - INI
        ' valorizacion de programa de telares
        Public Function fnc_programaciontejeduria_ImportarValorizacionFinal_V2(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Usuario", pvch_Usuario}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_Valorizacion_Importar_V2", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function
        'CAMBIO DG - NUEVA VERSION VALORIZACION - FIN
        ' consulta precio promedio en una rango de fechas, segun calidad
        Public Function fnc_programaciontejeduria_BusquedaCostoPromedio(ByVal pstrCodigo30Digitos As String, ByVal pstrFechaInicio As String, ByVal pstrFechaFin As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_CodigoLargo", pstrCodigo30Digitos,
                                                  "dtm_FechaInicio", pstrFechaInicio,
                                                  "dtm_FechaFin", pstrFechaFin}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_PrecioCalidad_Consultar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' consulta la valorizacion del programa
        Public Function fnc_programaciontejeduria_ConsultarValorizacionFinal(ByVal pstr_TipoBusqueda As String, ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                Select Case pstr_TipoBusqueda
                    Case "DETALLE" : ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ValorizacionDetalle_Consultar", lobjparametros)
                    Case "TOTALES" : ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ValorizacionTotales_Consultar", lobjparametros)
                    Case "RESUMEN" : ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ValorizacionResumen_Consultar", lobjparametros)
                    Case Else : ldtbProgramas = Nothing
                End Select
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function
        'CAMBIO DG - NUEVA VERSION VALORIZACION - INI
        ' consulta la valorizacion del programa
        Public Function fnc_programaciontejeduria_ConsultarValorizacionFinal_V2(ByVal pstr_TipoBusqueda As String, ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                Select Case pstr_TipoBusqueda
                    Case "DETALLE" : ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ValorizacionDetalle_Consultar_V2", lobjparametros)
                    Case "TOTALES" : ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ValorizacionTotales_Consultar", lobjparametros)
                    Case "RESUMEN" : ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ValorizacionResumen_Consultar_V2", lobjparametros)
                    Case Else : ldtbProgramas = Nothing
                End Select
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function
        'CAMBIO DG - NUEVA VERSION VALORIZACION - FIN
        ' actualiza datos de un articulo en la valorizacion
        Public Function fnc_programaciontejeduria_ActualizaRegValorizacionFinal(ByVal pstr_Fecha As String _
                                                                               , ByVal pstr_Version As Integer _
                                                                               , ByVal pstr_CodigoArticulo30 As String _
                                                                               , ByVal pdbl_Mts100kp As Double _
                                                                               , ByVal pdbl_PrecioPromedio As Double _
                                                                               , ByVal pdbl_CostoTotal As Double _
                                                                               , ByVal pdbl_CostoProduccion As Double _
                                                                               , ByVal pdbl_CostoVariable As Double _
                                                                               , ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                    "pint_VersionPrograma", pstr_Version,
                                                    "pvch_CodigoArticuloLargo", pstr_CodigoArticulo30,
                                                    "pnum_Mts100kp", pdbl_Mts100kp,
                                                    "pnum_PrecioPromedio", pdbl_PrecioPromedio,
                                                    "pnum_CostoTotal", pdbl_CostoTotal,
                                                    "pnum_CostoProduccion", pdbl_CostoProduccion,
                                                    "pnum_CostoVariable", pdbl_CostoVariable,
                                                    "pvch_Usuario", pvch_Usuario}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_Valorizacion_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        ' actualizaos parametro y totales respectivos - valorizacion de programa de telares
        Public Function fnc_programaciontejeduria_ActualizaParametroValorizacionFinal(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pstrParametro As Double, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pnum_CostosFijosReales", pstrParametro,
                                                  "pvch_Usuario", pvch_Usuario}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_Parametro_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try

            Return lstrResult
        End Function

        ' consulta articulos acabados
        Public Function fnc_programaciontejeduria_ConsultarArticulosAcabados(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldstDeterminarArticulo As DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldstDeterminarArticulo = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_ListaArticulosAcabados_2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstDeterminarArticulo = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldstDeterminarArticulo
        End Function
        '--------------------------------------------------------------------------------------------------------------------------------
        'CAMBIO DG - NUEVO MODULO DE PROCENTAJES 2DA Y PRECIOS 2DA - INI
        '--------------------------------------------------------------------------------------------------------------------------------
        ' consulta articulos acabados
        Public Function fnc_programaciontejeduria_ConsultarPorc2daYPrecio2da(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataTable
            Dim dtTable As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                dtTable = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_consulta_PORC2DA_PREC2DA", lobjparametros)
            Catch ex As Exception
                Throw ex
                dtTable = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return dtTable
        End Function
        ' determinacion de articulos a 30 digitos y vigencia
        Public Function fnc_programaciontejeduria_ImportarPorc2daYPrecio2da(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, _
                                                                            ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_PORC2DA_PREC2DA", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function
        'cambia estado a proceso de articulos acabados
        Public Function fnc_programaciontejeduria_CambiarEstadoPorc2daYPrecio2da(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pstr_Estado As String, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Estado", pstr_Estado,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_Estado_Porc2daYPrecios2da", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function
        'actualiza a proceso de articulos acabados
        Public Function fnc_programaciontejeduria_ActualizaPorc2daYPrecio2da(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pstr_Planta As String, ByVal pstr_Art20 As String, ByVal pstr_CodArt As String, ByVal pnum_Por2da As Decimal, ByVal pnum_Prec2da As Decimal) As Boolean
            Dim lstrResult As Boolean
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Planta", pstr_Planta,
                                                  "pvch_Art20", pstr_Art20,
                                                  "pvch_CodArt", pstr_CodArt,
                                                  "pnum_Por2da", pnum_Por2da,
                                                  "pnum_Prec2da", pnum_Prec2da}

                lobjaccesoDatos.EjecutarComando("usp_tel_programatelares_update_PORC2DA_PREC2DA", lobjparametros)
                lstrResult = True
            Catch ex As Exception
                Throw ex
                lstrResult = False
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        '--------------------------------------------------------------------------------------------------------------------------------
        'CAMBIO DG - NUEVO MODULO DE PROCENTAJES 2DA Y PRECIOS 2DA - FIN
        '--------------------------------------------------------------------------------------------------------------------------------
        ' actualizar estado principal del programa
        Public Function fnc_ActualizaPrincipal(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                               ByVal pstr_Principal As String, ByVal pstr_usuario As String) As DataTable
            Dim ldtbPrincipal As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pchr_Principal", pstr_Principal, _
                                                  "pvch_Usuario", pstr_usuario}
                ldtbPrincipal = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_CabeceraActualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbPrincipal = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbPrincipal
        End Function

        ' consulta programa por rango de fechas.
        Public Function fnc_programaciontejeduria_ConsultarRequisiciones_Aprobacion_x_Usuario(ByVal pstr_FechaDesde As String, ByVal pstr_FechaHasta As String, ByVal pstr_Estado As String, ByVal pstrUsuario As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_Fecha_Desde", pstr_FechaDesde,
                                                  "pvch_Fecha_Hasta", pstr_FechaHasta,
                                                  "vch_Estado", pstr_Estado,
                                                  "vch_Usuario", pstrUsuario}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ConsultaRequisicion_Aprobacion_x_Usuario_2", lobjparametros)
            Catch ex As Exception
                ldtbProgramas = Nothing
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' aprobacion / desaprobacion de la requisiciones de precostos
        Public Function fnc_programaciontejeduria_ActualizaRequisicion_Aprobaciones(ByVal pstr_Accion As String _
                                                                                 , ByVal pstr_NU_SOLI_SECU As Integer _
                                                                                 , ByVal pstr_NU_PASO As String _
                                                                                 , ByVal pstr_NU_DOCU As String _
                                                                                 , ByVal pstr_FechaPrograma As String _
                                                                                 , ByVal pstr_VersionPrograma As String _
                                                                                 , ByVal pstr_NumeroRequisicion As String _
                                                                                 , ByVal pstr_NumeroSecuencia As String _
                                                                                 , ByVal pstr_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_Accion", pstr_Accion,
                                                  "pint_NU_SOLI_SECU", pstr_NU_SOLI_SECU,
                                                  "pint_NU_PASO", pstr_NU_PASO,
                                                  "pvch_NU_DOCU", pstr_NU_DOCU,
                                                  "pvch_FechaPrograma", pstr_FechaPrograma,
                                                  "pint_VersionPrograma", pstr_VersionPrograma,
                                                  "pvch_NumeroRequisicion", pstr_NumeroRequisicion,
                                                  "pvch_NumeroSecuencia", pstr_NumeroSecuencia,
                                                  "pvch_Usuario", pstr_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_ActualizaRequisicion_Aprobaciones_2", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        ' listamos los datos del detalle de la requisicion
        Public Function fnc_programaciontejeduria_BuscaDatosDetalleRequisicion(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer,
                                                                          ByVal pstr_NumRequisicion As String, ByVal pstrNumeroSecuencia As String,
                                                                          ByVal pstrNU_SOLI_SECU As String, ByVal pstrNU_PASO As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_NumRequisicion", pstr_NumRequisicion,
                                                  "pint_NumeroSecuencia", pstrNumeroSecuencia,
                                                  "pint_NU_SOLI_SECU", pstrNU_SOLI_SECU,
                                                  "pvch_NU_PASO", pstrNU_PASO}

                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ListaDatosDetalleRequisicion_2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' actualiza los metros con factor de engomado
        Public Function fnc_programaciontejeduria_ActualizaRegMetrosAcabados(ByVal pstr_Fecha As String _
                                                                           , ByVal pstr_Version As Integer _
                                                                           , ByVal pstr_CodigoPlanta As String _
                                                                           , ByVal pstr_CodigoTipoMaquina As String _
                                                                           , ByVal pstr_CodigoArticulo As String _
                                                                           , ByVal pnum_PorcentajeEncogimiento As Double _
                                                                           , ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_CodigoPlanta", pstr_CodigoPlanta,
                                                  "pvch_CodigoTipoMaquina", pstr_CodigoTipoMaquina,
                                                  "pvch_CodigoArticulo", pstr_CodigoArticulo,
                                                  "pnum_PorcentajeEngomiento", pnum_PorcentajeEncogimiento,
                                                  "pvch_Usuario", pvch_Usuario}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegMetrosAcabados_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        ' elimina articulo en metros acabados.
        Public Function fnc_programaciontejeduria_EliminaRegMetrosAcabados(ByVal pstr_Fecha As String _
                                                                           , ByVal pstr_Version As Integer _
                                                                           , ByVal pstr_CodigoPlanta As String _
                                                                           , ByVal pstr_CodigoTipoMaquina As String _
                                                                           , ByVal pstr_CodigoArticulo As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_CodigoPlanta", pstr_CodigoPlanta,
                                                  "pvch_CodigoTipoMaquina", pstr_CodigoTipoMaquina,
                                                  "pvch_CodigoArticulo", pstr_CodigoArticulo}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegMetrosAcabados_Eliminar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        'cambia estado al proceso de metros acabados.
        Public Function fnc_programaciontejeduria_CambiarEstadoMetrosAcabados(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pstr_Estado As String, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Estado", pstr_Estado,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegMetrosAcabados_ActualizarEstado", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        'actualizar articulos en metros acabados
        Public Function fnc_programaciontejeduria_ActualizaRegArticulosAcabados(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer,
                                                                               ByVal pstr_CodigoPlanta As String, pstr_CodigoCrudo As String,
                                                                               ByVal pstr_CodigoArticulo7 As String, ByVal pvch_Usuario As String,
                                                                               ByVal pxml_ListadoArticulosEdicion As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_CodigoPlanta", pstr_CodigoPlanta,
                                                  "pvch_CodigoCrudo", pstr_CodigoCrudo,
                                                  "pvch_CodigoArticulo7", pstr_CodigoArticulo7,
                                                  "pvch_Usuario", pvch_Usuario,
                                                  "pxml_ListadoEdicionXML", pxml_ListadoArticulosEdicion}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegArticulosAcabados_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        ' devuleve articulo a 20 digitos
        Public Function fnc_programaciontejeduria_ValidaFichaRequisicion(ByVal pstr_CodigoFicha As String) As DataTable
            Dim ldtbFicha As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pstr_CodigoFicha", pstr_CodigoFicha}

                ldtbFicha = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ValidaFichaRequisicion", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbFicha
        End Function

        ' obtenemos datos de un articulo para editar en memoria
        Public Function fnc_programaciontejeduria_BuscaArticulosAcabadosEdicion(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer,
                                                                               ByVal pstr_Planta As String, ByVal pstr_CodigoCrudo As String,
                                                                               ByVal pstrCodigo7Digitos As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Planta", pstr_Planta,
                                                  "pvch_CodigoCrudo", pstr_CodigoCrudo,
                                                  "pvch_Codigo7Digitos", pstrCodigo7Digitos}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ListaArticulosAcabadosEdicion", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' consulta articulo largo
        Public Function fnc_programaciontejeduria_BusquedaArticulo30(ByVal pstrCodigo7Digitos As String, ByVal pstrDescripcion As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_Codigo7Digitos", pstrCodigo7Digitos,
                                                  "pvch_Descripcion", pstrDescripcion}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelaresdetalle_BuscarArticulo30", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' consulta requisicion de secuencia de articulo
        Public Function fnc_programaciontejeduria_ConsultarRequisicionPrograma(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ListaRequisicionPrograma", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' lista articulos para edicion de requisicion
        Public Function fnc_programaciontejeduria_BuscaArticulosRequisicionEdicion(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer,
                                                                                  ByVal pstr_NumRequisicion As String, ByVal pstr_CodigoArticuloCrudo As String) As DataTable

            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_NumRequisicion", pstr_NumRequisicion,
                                                  "pvch_CodigoArticuloCrudo", pstr_CodigoArticuloCrudo}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ListaArticulosRequisicionEdicion", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        ' consultas varias para la edicion de requisicion de precosto.
        Public Function fnc_programaciontejeduria_BusquedaRegistros(ByVal pstrCodigo As String, ByVal pstrDescripcion As String, ByVal pstrTipoBusqueda As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lstrQuery As String
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_Codigo", pstrCodigo,
                                                  "pvch_Descripcion", pstrDescripcion}
                Select Case pstrTipoBusqueda
                    Case "ARTICULO_BASE"
                        lstrQuery = "usp_tel_programatelaresdetalle_BuscarArticuloBase"
                    Case "ACABADO"
                        lstrQuery = "usp_tel_programatelaresdetalle_BuscarListaAcabado"
                    Case "COLOR"
                        lstrQuery = "usp_tel_programatelaresdetalle_BuscarListaColor"
                    Case "COLORANTE"
                        lstrQuery = "usp_tel_programatelaresdetalle_BuscarListaColorante"
                    Case Else
                        Throw New Exception("ERROR: Imposible realizar la operación. Contactese con el area de sistemas.")
                End Select
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable(lstrQuery, lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbProgramas = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function

        'valida datos para registro de requisicion
        Public Function fnc_programaciontejeduria_VerificaDataIngresada(ByVal pstrTipoBusqueda As String, ByVal pstrValor As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_TipoBusqueda", pstrTipoBusqueda,
                                                  "pvch_Valor", pstrValor}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegRequerimientos_VerificaData", lobjparametros)
            Catch ex As Exception
                lstrResult = ex.Message
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        'actualiza requisicion de precostos en el programa
        Public Function fnc_programaciontejeduria_ActualizaRegRequisicionPrograma(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer,
                                                                               ByVal pstrNumeroRequisicion As String, pstr_CodigoCrudo As String,
                                                                               ByVal pvch_Usuario As String, ByVal pxml_ListadoArticulosEdicion As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_NumeroRequisicion", pstrNumeroRequisicion,
                                                  "pvch_CodigoCrudo", pstr_CodigoCrudo,
                                                  "pvch_Usuario", pvch_Usuario,
                                                  "pxml_ListadoEdicionXML", pxml_ListadoArticulosEdicion}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegRequisicionPrograma_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        'actualiza requisicion de precostos
        Public Function fnc_programaciontejeduria_ActualizaRegFichaRequisicion(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer,
                                                                       ByVal pstrNumeroRequisicion As String, pstr_CodigoCrudo As String,
                                                                       ByVal pvch_Usuario As String, ByVal pxml_ListadoArticulosEdicion As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_NumeroRequisicion", pstrNumeroRequisicion,
                                                  "pvch_CodigoCrudo", pstr_CodigoCrudo,
                                                  "pvch_Usuario", pvch_Usuario,
                                                  "pxml_ListadoEdicionXML", pxml_ListadoArticulosEdicion}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegFichaRequisicion_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        'cambia estado a proceso de articulos acabados
        Public Function fnc_programaciontejeduria_CambiarEstadoArticulosAcabados(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pstr_Estado As String, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Estado", pstr_Estado,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegArticulosAcabados_ActualizarEstado", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        'cambia estado a proceso de valorizacion final
        Public Function fnc_programaciontejeduria_CambiarEstadoValorizacionFinal(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, ByVal pstr_Estado As String, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Estado", pstr_Estado,
                                                  "pvch_Usuario", pvch_Usuario}
                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_RegArticulosAcabados_ActualizarEstado", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        ' importar ficha articulo
        Public Function fnc_ArticuloFicha_Importar(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                                   ByVal pvch_Usuario As String, strTipo As String) As DataTable
            Dim ldtbFichaImportar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario, _
                                                  "pvch_TipoFicha", strTipo}
                ldtbFichaImportar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticuloFicha_Importar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbFichaImportar = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbFichaImportar
        End Function
        'CAMBIO DG - NUEVA VERSION DE PROGRAMA DE TELARES - INI
        ' importar ficha articulo
        Public Function fnc_ArticuloFicha_Importar_V2(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                                   ByVal pvch_Usuario As String, strTipo As String) As DataTable
            Dim ldtbFichaImportar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario, _
                                                  "pvch_TipoFicha", strTipo}
                ldtbFichaImportar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticuloFicha_Importar_V2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbFichaImportar = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbFichaImportar
        End Function
        'CAMBIO DG - NUEVA VERSION DE PROGRAMA DE TELARES - FIN
        ' consulta articulo - ficha de costos
        Public Function fnc_ArticuloFicha_Consultar(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldstArticuloFicha As DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldstArticuloFicha = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_ArticuloFicha_Consultar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstArticuloFicha = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldstArticuloFicha
        End Function
        'CAMBIO DG - NUEVA VERSION DE PROGRAMA DE TELARES - INI
        ' consulta articulo - ficha de costos
        Public Function fnc_ArticuloFicha_Consultar_V2(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldstArticuloFicha As DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldstArticuloFicha = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_ArticuloFicha_Consultar_V2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstArticuloFicha = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldstArticuloFicha
        End Function
        'CAMBIO DG - NUEVA VERSION DE PROGRAMA DE TELARES - FIN
        ' generar masiva - ficha de costos
        Public Function fnc_ArticuloFicha_GenerarMasiva(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pvch_Usuario As String) As DataTable
            Dim ldtbGeneraFicha As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtbGeneraFicha = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_GeneraMasiva", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbGeneraFicha = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbGeneraFicha
        End Function
        'CAMBIO DG - NUEVA VERSION DE PROGRAMA DE TELARES - INI
        Public Function fnc_ArticuloFicha_GenerarMasiva_V2(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pvch_Usuario As String) As DataTable
            Dim ldtbGeneraFicha As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtbGeneraFicha = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_GeneraMasiva_V2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbGeneraFicha = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbGeneraFicha
        End Function
        'CAMBIO DG - NUEVA VERSION DE PROGRAMA DE TELARES - FIN
        ' actualizamos ficha en articulo/articulo
        Public Function fnc_ActualizarFichaCosto(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pvch_Usuario As String) As DataTable
            Dim ldtbActualizaFicha As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtbActualizaFicha = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ActualizaMasiva", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbActualizaFicha = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbActualizaFicha
        End Function

        ' consulta articulos equivalentes
        Public Function fnc_programaciontejeduria_ArticuloFichaGeneraAprobacion(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pvch_Usuario As String) As DataTable
            Dim ldtbArtEquivalente As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtbArtEquivalente = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticuloFicha_GeneraAprobacion_2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbArtEquivalente = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbArtEquivalente
        End Function

        ' consulta acabados
        Public Function fnc_programaciontejeduria_AcabadosConsltar() As DataTable
            Dim ldtbCalidades As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                ldtbCalidades = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_AcabadosConsultar")
            Catch ex As Exception
                Throw ex
                ldtbCalidades = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbCalidades
        End Function

        ' importar articulo - precio
        Public Function fnc_ArticuloPrecio_Importar(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pvch_Usuario As String, _
                                                    ByVal strFechaInicial As String, ByVal strFechaFinal As String, _
                                                    ByVal intMetros As Integer, ByVal strAcabado As String) As DataTable
            Dim ldtbFichaImportar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario, _
                                                  "pdtm_FechaInicial", strFechaInicial, _
                                                  "pdtm_FechaFinal", strFechaFinal, _
                                                  "pnum_Metros", intMetros, _
                                                  "pvch_Calidad", strAcabado}
                ldtbFichaImportar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_Importar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbFichaImportar = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbFichaImportar
        End Function
        ' CAMBIO DG - NUEVA VERSION DE VALORIZACION -  INI
        ' importar articulo - precio
        Public Function fnc_ArticuloPrecio_Importar_V2(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pvch_Usuario As String, _
                                                    ByVal strFechaInicial As String, ByVal strFechaFinal As String, _
                                                    ByVal intMetros As Integer, ByVal strAcabado As String) As DataTable
            Dim ldtbFichaImportar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario, _
                                                  "pdtm_FechaInicial", strFechaInicial, _
                                                  "pdtm_FechaFinal", strFechaFinal, _
                                                  "pnum_Metros", intMetros, _
                                                  "pvch_Calidad", strAcabado}
                ldtbFichaImportar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_Importar_V2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbFichaImportar = Nothing
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbFichaImportar
        End Function
        ' CAMBIO DG - NUEVA VERSION DE VALORIZACION -  FIN
        ' actualizar articulo - precio - ig
        Public Function fnc_ArticuloPrecio_Actualizar(ByVal pstr_Fecha As String, ByVal pstr_Version As String, strCodigoLargo As String, _
                                                    ByVal dblPrecioPrimera As Double, ByVal dblPrecioNoPrimera As Double, ByVal dblPorcentajeNoPrimera As Double, _
                                                    ByVal pvch_Usuario As String) As DataTable
            Dim ldtPrecioActualizar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha, _
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_CodigoArticulo30", strCodigoLargo, _
                                                  "pnum_PrecioPrimera", dblPrecioPrimera, _
                                                  "pnum_PrecioNoPrimera", dblPrecioNoPrimera, _
                                                  "pnum_PorcentajeNoPrimera", dblPorcentajeNoPrimera, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtPrecioActualizar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtPrecioActualizar = Nothing
            End Try
            Return ldtPrecioActualizar
        End Function

        ' actualizar articulo - precio - vta
        Public Function fnc_ArticuloPrecio_Modificar(ByVal pstr_Fecha As String, ByVal pstr_Version As String, strCodigoLargo As String, _
                                                    ByVal dblPrecioUnitarioLocal As Double, ByVal dblPorcentajeLocal As Double, _
                                                    ByVal dblPrecioUnitarioExportacion As Double, ByVal dblPorcentajeExportacion As Double, _
                                                    ByVal pvch_Usuario As String) As DataTable
            Dim ldtPrecioModificar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha, _
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_CodigoArticulo30", strCodigoLargo, _
                                                  "pnum_PrecioUnitarioLocal", dblPrecioUnitarioLocal, _
                                                  "pnum_PorcentajeLocal", dblPorcentajeLocal, _
                                                  "pnum_PrecioUnitarioExportacion", dblPrecioUnitarioExportacion, _
                                                  "pnum_PorcentajeExportacion", dblPorcentajeExportacion, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtPrecioModificar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_Actualizar_2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtPrecioModificar = Nothing
            End Try
            Return ldtPrecioModificar
        End Function

        ' consulta articulos - precios ig/venta
        Public Function fnc_programaciontejeduria_ArticuloPrecioConsultar(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                                                          strTipo As String) As DataSet
            Dim ldstArticuloPrecio As DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pchr_FlagPrecio", strTipo}
                ldstArticuloPrecio = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_ArticulosPrecios_Consultar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstArticuloPrecio = Nothing
            End Try
            Return ldstArticuloPrecio
        End Function

        ' consulta articulos - precios(vigentes y nuevo)
        Public Function fnc_programaciontejeduria_ArticuloPrecioConsultar2(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldstArticuloPrecio As DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldstArticuloPrecio = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_ArticulosPrecios_Consultar_2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstArticuloPrecio = Nothing
            End Try
            Return ldstArticuloPrecio
        End Function
        'CAMBIO DG - NUEVA VERSION DE VALORIZACION - INI
        ' consulta articulos - precios(vigentes y nuevo)
        Public Function fnc_programaciontejeduria_ArticuloPrecioConsultar2_V2(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldstArticuloPrecio As DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldstArticuloPrecio = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_ArticulosPrecios_Consultar_2_V2", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstArticuloPrecio = Nothing
            End Try
            Return ldstArticuloPrecio
        End Function
        'CAMBIO DG - NUEVA VERSION DE VALORIZACION - FIN

        'cambia estado a proceso de generacion de fichas
        Public Function fnc_programaciontejeduria_CambiarEstadoArticuloFicha(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, _
                                                                             ByVal pstr_Estado As String, ByVal pvch_Usuario As String) As String
            Dim lstrRespuesta As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Estado", pstr_Estado,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrRespuesta = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_ArticuloFicha_ActualizarEstado", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrRespuesta
        End Function

        ' ejecutar envio de email - articulos precio 
        Public Function fnc_programaciontejeduria_ArticuloPrecioEmail(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                                                          pstrUsuario As String) As DataTable
            Dim ldtbArticuloPrecioEmail As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pstrUsuario}
                ldtbArticuloPrecioEmail = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPreciosEmail", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbArticuloPrecioEmail = Nothing
            End Try
            Return ldtbArticuloPrecioEmail
        End Function

        ' ejecutar envio de email - requisiciones
        Public Function fnc_programaciontejeduria_RequisicionEmail(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                                                          pstrUsuario As String) As DataTable
            Dim ldtbArticuloPrecioEmail As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pstrUsuario}
                ldtbArticuloPrecioEmail = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_RegRequisicionProgramaEmail", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbArticuloPrecioEmail = Nothing
            End Try
            Return ldtbArticuloPrecioEmail
        End Function

        ' consulta programa por rango de fechas.
        Public Function fnc_programaciontejeduria_ConsultarRequisiciones_Aprobacion_x_Usuario_2(ByVal pstrRequisicion As String, _
                                                                                                ByVal pstrSecuencia As String, _
                                                                                                ByVal pstrCodigoLargo As String, _
                                                                                                ByVal pstrUsuario As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_NumeroRequisicion", pstrRequisicion,
                                                  "pint_Secuencia", pstrSecuencia,
                                                  "pvch_CodigoLargo", pstrCodigoLargo,
                                                  "pvch_Usuario", pstrUsuario}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ConsultaRequisicion_Aprobacion_x_Usuario_3", lobjparametros)
            Catch ex As Exception
                ldtbProgramas = Nothing
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function
        'CAMBIO DG - INI
        ' consulta programa por rango de fechas.
        Public Function fnc_programaciontejeduria_ConsultarRequisiciones_Aprobacion_x_Usuario_2_V2(ByVal pstrRequisicion As String, _
                                                                                                ByVal pstrSecuencia As String, _
                                                                                                ByVal pstrCodigoLargo As String, _
                                                                                                ByVal pstrUsuario As String) As DataTable
            Dim ldtbProgramas As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_NumeroRequisicion", pstrRequisicion,
                                                  "pint_Secuencia", pstrSecuencia,
                                                  "pvch_CodigoLargo", pstrCodigoLargo,
                                                  "pvch_Usuario", pstrUsuario}
                ldtbProgramas = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ConsultaRequisicion_Aprobacion_x_Usuario_3_V2", lobjparametros)
            Catch ex As Exception
                ldtbProgramas = Nothing
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return ldtbProgramas
        End Function
        'CAMBIO DG - FIN
        ' actualizar % - segunda - acabado - ig
        Public Function fnc_ArticuloSegundaAcabado_Actualizar(ByVal pstr_Fecha As String, ByVal pstr_Version As String, strCodigoLargo As String, _
                                                    ByVal dblPorcentajeNoPrimera As Double, ByVal pvch_Usuario As String) As DataTable
            Dim ldtPrecioActualizar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha, _
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_CodigoArticulo30", strCodigoLargo, _
                                                  "num_PorcentajeNoPrimeraAcabado", dblPorcentajeNoPrimera, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtPrecioActualizar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_Actualizar_3", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtPrecioActualizar = Nothing
            End Try
            Return ldtPrecioActualizar
        End Function

        'cambia estado a proceso determinacion de precios
        Public Function fnc_programaciontejeduria_CambiarEstadoArticulosPrecios(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, _
                                                                                ByVal pstr_Estado As String, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Estado", pstr_Estado,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_ArticuloPrecio_ActualizarEstado", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        'cambia estado a proceso valorizacion
        Public Function fnc_programaciontejeduria_CambiarEstadoValorizacion(ByVal pstr_Fecha As String, ByVal pstr_Version As Integer, _
                                                                                ByVal pstr_Estado As String, ByVal pvch_Usuario As String) As String
            Dim lstrResult As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Estado", pstr_Estado,
                                                  "pvch_Usuario", pvch_Usuario}

                lstrResult = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_Valorizacion_Estado", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return lstrResult
        End Function

        ' actualizar % - segunda - acabado - origin
        Public Function fnc_ArticuloSegundaAcabado_Origen(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                                          ByVal pvch_Usuario As String) As DataTable
            Dim ldtPrecioActualizar As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha, _
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pvch_Usuario}
                ldtPrecioActualizar = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_Actualizar_4", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtPrecioActualizar = Nothing
            End Try
            Return ldtPrecioActualizar
        End Function

        ' ejecutar envio de email - articulos nuevos - determinacion de articulos
        Public Function fnc_programaciontejeduria_AcabadosEmail(ByVal pstr_Fecha As String, ByVal pstr_Version As String, _
                                                                          pstrUsuario As String) As DataTable
            Dim ldtbArticuloEmail As DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version, _
                                                  "pvch_Usuario", pstrUsuario}
                ldtbArticuloEmail = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_DeterminacionArticuloEmail", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbArticuloEmail = Nothing
            End Try
            Return ldtbArticuloEmail
        End Function

        ' consulta calculo de % segundas y otros - calculo de precios
        Public Function fnc_programaciontejeduria_CalculoSegundasOtrosConsultar(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldstSegundaotros As New DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldstSegundaotros = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_PorcentajeSegundas_CalidadArticulo_Consultar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstSegundaotros = Nothing
            End Try
            Return ldstSegundaotros
        End Function

        ' importar calculo de % segundas y otros - calculo de precios
        Public Function fnc_programaciontejeduria_CalculoSegundasOtrosImportar(ByVal pstr_Fecha As String, ByVal pint_Version As Integer, _
                                                                        ByVal pstr_FechaInicio As String, ByVal pstr_FechaFin As String, _
                                                                        ByVal pstr_Usuario As String) As DataTable
            Dim ldtbSegundaotros As New DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtbSegundaotros = Nothing
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pint_Version, _
                                                  "vch_FechaInicio", pstr_FechaInicio, _
                                                  "vch_FechaFin", pstr_FechaFin, _
                                                  "vch_Usuario", pstr_Usuario}
                ldtbSegundaotros = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_PorcentajeSegundas_CalidadArticulo_Importar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbSegundaotros = Nothing
            End Try
            Return ldtbSegundaotros
        End Function

        ' consulta % segundas y otros - calculo de precios
        Public Function fnc_programaciontejeduria_SegundasOtrosConsultar(ByVal pstr_Fecha As String, ByVal pstr_Version As String) As DataSet
            Dim ldstSegundaotros As New DataSet
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version}
                ldstSegundaotros = lobjaccesoDatos.ObtenerDataSet("usp_tel_programatelares_PorcentajeSegundas_Consultar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldstSegundaotros = Nothing
            End Try
            Return ldstSegundaotros
        End Function

        ' actualizar % segundas y otros - calculo de precios
        Public Function fnc_programaciontejeduria_SegundasOtrosActualizar(ByVal pstr_Fecha As String, ByVal pint_Version As Integer, _
                                                                        ByVal pstr_FestrArticulo7 As String, ByVal dblSegundaRF As Double, _
                                                                        ByVal pstr_Usuario As String) As DataTable
            Dim ldtbSegundaOtros As New DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtbSegundaOtros = Nothing
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pint_Version, _
                                                  "vch_CodigoArticulo", pstr_FestrArticulo7, _
                                                  "num_PorSegOtrosRF", dblSegundaRF, _
                                                  "vch_Usuario", pstr_Usuario}
                ldtbSegundaOtros = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_PorcentajeSegundas_Actualizar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbSegundaOtros = Nothing
            End Try
            Return ldtbSegundaOtros
        End Function

        ' importar % segundas y otros - calculo de precios
        Public Function fnc_programaciontejeduria_SegundasOtrosImportar(ByVal pstr_Fecha As String, ByVal pint_Version As Integer, ByVal pstr_Usuario As String) As DataTable
            Dim ldtbSegundaOtros As New DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtbSegundaOtros = Nothing
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pint_Version, _
                                                  "pvch_Usuario", pstr_Usuario}
                ldtbSegundaOtros = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_PorcentajeSegundas_Importar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbSegundaOtros = Nothing
            End Try
            Return ldtbSegundaOtros
        End Function

        ' estado % segundas y otros - calculo de precios
        Public Function fnc_programaciontejeduria_SegundasOtrosEstado(ByVal pstr_Fecha As String, ByVal pint_Version As Integer, _
                                                                      ByVal pstrEstado As String, ByVal pstr_Usuario As String) As DataTable
            Dim ldtbSegundaOtros As New DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtbSegundaOtros = Nothing
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pint_Version, _
                                                  "pvch_Estado", pstrEstado, _
                                                  "pvch_Usuario", pstr_Usuario}
                ldtbSegundaOtros = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_PorcentajeSegundas_Estado", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbSegundaOtros = Nothing
            End Try
            Return ldtbSegundaOtros
        End Function

        ' estado % segundas y otros - calculo de precios
        Public Function fnc_programaciontejeduria_SegundasOtrosActualizaMasiva(ByVal pstr_Fecha As String, ByVal pint_Version As Integer, _
                                                                      ByVal pstrXML As String, ByVal pstr_Usuario As String) As DataTable
            Dim ldtbSegundaOtros As New DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtbSegundaOtros = Nothing
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pint_Version, _
                                                  "pvch_Xml", pstrXML, _
                                                  "pvch_Usuario", pstr_Usuario}
                ldtbSegundaOtros = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_PorcentajeSegundas_ActualizarMasivo", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbSegundaOtros = Nothing
            End Try
            Return ldtbSegundaOtros
        End Function

        ' importar % segundas y otros - calculo de precios
        Public Function fnc__ArticuloPrecio_SegundasOtrosImportar(ByVal pstr_Fecha As String, ByVal pint_Version As Integer, ByVal pstr_Usuario As String) As DataTable
            Dim ldtbSegundaOtros As New DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtbSegundaOtros = Nothing
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pint_Version, _
                                                  "pvch_Usuario", pstr_Usuario}
                ldtbSegundaOtros = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_SegundasImportar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbSegundaOtros = Nothing
            End Try
            Return ldtbSegundaOtros
        End Function

        ' estado % segundas y otros - calculo de precios
        Public Function fnc__ArticuloPrecio_SegundasOtrosActualizaMasiva(ByVal pstr_Fecha As String, ByVal pint_Version As Integer, _
                                                                      ByVal pstrXML As String, ByVal pstr_Usuario As String) As DataTable
            Dim ldtbSegundaOtros As New DataTable
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtbSegundaOtros = Nothing
            Try
                Dim lobjparametros As Object() = {"pvch_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pint_Version, _
                                                  "pvch_Xml", pstrXML, _
                                                  "pvch_Usuario", pstr_Usuario}
                ldtbSegundaOtros = lobjaccesoDatos.ObtenerDataTable("usp_tel_programatelares_ArticulosPrecios_SegundasActualizarMasivo", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtbSegundaOtros = Nothing
            End Try
            Return ldtbSegundaOtros
        End Function

        'ADD LUIS_AJ (20210409)
        ' actualiza descripcion del programa de telares
        Public Function fnc_programaciontejeduria_ModificaDescripcionPrograma(ByVal pstr_Fecha As String, ByVal pstr_Version As String, ByVal pstr_Descripcion As String, ByVal pstr_Usuario As String) As String
            Dim strResultado As String = ""
            Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
            Try
                Dim lobjparametros As Object() = {"pdtm_FechaPrograma", pstr_Fecha,
                                                  "pint_VersionPrograma", pstr_Version,
                                                  "pvch_Descripcion", pstr_Descripcion,
                                                  "pvch_Usuario", pstr_Usuario
                                                 }
                strResultado = lobjaccesoDatos.ObtenerValor("usp_tel_programatelares_actualizadescripcion", lobjparametros)
            Catch ex As Exception
                Throw ex
            Finally
                lobjaccesoDatos = Nothing
            End Try
            Return strResultado
        End Function

        '<><><><><><><><><><> ---Fin: Nuevo modelo para el programa de telares--- <><><><><><><><><><>
#End Region

#Region "Programa de telares - anterior"

        Public Function leerPogramacionTejeduria(ByVal pDteFecha As Date) As DataTable
            Dim tbProgramacion As DataTable
            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"FechaIn", pDteFecha}
                tbProgramacion = accesoDatos.ObtenerDataTable("NM_GET_PROGRAMACION_TELARES", objParametros)
            Catch ex As Exception
                Throw ex
                tbProgramacion = Nothing
            End Try
            Return tbProgramacion
        End Function

        Public Function fnc_obtenerultimoprograma() As DataTable
            Dim ldtbprogramacion As DataTable
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                ldtbprogramacion = lobjaccesoDatos.ObtenerDataTable("usp_tel_progtelaresultimo_obtener")
            Catch ex As Exception
                Throw ex
                ldtbprogramacion = Nothing
            End Try
            Return ldtbprogramacion
        End Function

        Public Function fnc_programaciontejeduriainformediario_listar(ByVal pint_tipoconsulta As Int16, ByVal pstr_fechaprograma As String) As DataTable
            Dim ldtbprogramacion As DataTable
            Try

                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"ptin_tipoconsulta", pint_tipoconsulta, "pvch_fecha", pstr_fechaprograma}
                ldtbprogramacion = lobjaccesoDatos.ObtenerDataTable("usp_tel_progtelaresinfdiario_lista", lobjparametros)

            Catch ex As Exception
                Throw ex
                ldtbprogramacion = Nothing
            End Try

            Return ldtbprogramacion

        End Function

        Public Function fnc_programaciontejeduriainformediario_guardar(ByVal pint_accion As Integer, _
                                                                       ByVal pstr_fecha As String, _
                                                                       ByVal pstr_planta As String, _
                                                                       ByVal pstr_tipomaquina As String, _
                                                                       ByVal pstr_articulo As String, _
                                                                       ByVal pint_revisionarticulo As Integer, _
                                                                       ByVal pint_cantidad As Double, _
                                                                       ByVal pint_dias As Integer, _
                                                                       ByVal pdbl_metrostejer As Double, _
                                                                       ByVal pint_numtelas As Integer, _
                                                                       ByVal pstr_usuario As String, _
                                                                       ByVal pint_velocidadteorica As Integer, _
                                                                       ByVal pint_cantreal As Double, _
                                                                       ByVal pstr_observacion As String, _
                                                                       ByVal pint_meta As Integer, _
                                                                       ByVal pstr_inicio As String, _
                                                                       ByVal pdbl_idiariometrosreal As Double) As DataTable
            'Modificado:Se cambia el tipo de datos de telares programados y reales a numero real
            'Autor:Alezander Torres Cardenas
            'Fecha: Diciembre 2014
            Dim ldtberror As DataTable
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"ptin_accion", pint_accion, "pvch_fecha", pstr_fecha, "pvch_codigoplanta", pstr_planta, "pvch_codigotipomaquina", pstr_tipomaquina, "pvch_codigoarticulo", pstr_articulo, "pint_revisionarticulo", pint_revisionarticulo, "pint_cantidad", pint_cantidad, "pint_dias", pint_dias, "pnum_metrostejer", pdbl_metrostejer, "pint_numtelas", pint_numtelas, "pvch_usuario", pstr_usuario, "pint_velocidadteorica", pint_velocidadteorica, "pint_idiariocantidad", pint_cantreal, "pvch_idiarioobservacion", pstr_observacion, "pint_idiariometa", pint_meta, "pvch_idiarioinicio", pstr_inicio, "pnum_idiariometrosreal", pdbl_idiariometrosreal}

                ldtberror = lobjaccesoDatos.ObtenerDataTable("usp_tel_progtelaresinfdiario_guardar", lobjparametros)
            Catch ex As Exception
                Throw ex
                ldtberror = Nothing
            End Try
            Return ldtberror
        End Function

        Public Function fnc_programaciontejeduriainformediario_eliminar(ByVal pstr_fecha As String, ByVal pstr_planta As String, ByVal pstr_tipomaquina As String, ByVal pstr_articulo As String) As DataTable
            Dim ldtberror As DataTable
            Try
                Dim lobjaccesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim lobjparametros As Object() = {"pvch_fecha", pstr_fecha, "pvch_codigoplanta", pstr_planta, "pvch_codigotipomaquina", pstr_tipomaquina, "pvch_codigoarticulo", pstr_articulo}

                ldtberror = lobjaccesoDatos.ObtenerDataTable("usp_tel_progtelaresinfdiario_eliminar", lobjparametros)

            Catch ex As Exception
                Throw ex
                ldtberror = Nothing
            End Try

            Return ldtberror

        End Function

        Public Sub exportarDatos(ByRef registrosAfectados As Integer)

            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"FechaIn", fecha, "Usuario", usuario}
                registrosAfectados = accesoDatos.EjecutarComando("NM_EXPORTAR_PROGRAMA_TELA", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Private Function listarMaquinaTipo() As DataTable

            Dim dtMaquinaTipo As DataTable
            Dim maquinaTipo As New NM_MaquinaTipo

            Try
                dtMaquinaTipo = maquinaTipo.List()

            Catch ex As Exception
                Throw ex
                dtMaquinaTipo = Nothing
            End Try

            Return dtMaquinaTipo

        End Function

        Public Function listarMaquinaTipoDDL() As DataTable

            Dim dtMaquinaTipo As DataTable
            Dim drMaquinaTipo As DataRow

            Try
                dtMaquinaTipo = listarMaquinaTipo()
                drMaquinaTipo = dtMaquinaTipo.NewRow
                drMaquinaTipo("codigo_tipo_maquina") = "0"
                drMaquinaTipo("descripcion_tipo_maquina") = "Seleccionar"
                dtMaquinaTipo.Rows.InsertAt(drMaquinaTipo, 0)
            Catch ex As Exception
                dtMaquinaTipo = Nothing
                Throw ex
            End Try

            Return dtMaquinaTipo

        End Function

        Public Function listarArticulos() As DataTable

            Dim dtListaArticulos As DataTable

            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                dtListaArticulos = accesoDatos.ObtenerDataTable("NM_GET_LISTA_ARTICULO")
            Catch ex As Exception
                Throw ex
            End Try

            Return dtListaArticulos

        End Function

        Public Function buscarArticulo(ByVal CodArticulo As String) As DataTable

            Dim dtArticulo As DataTable
            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"CodigoArticulo", CodArticulo}
                dtArticulo = accesoDatos.ObtenerDataTable("NM_BUSCAR_ARTICULO", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtArticulo

        End Function

        Public Function getVelocidadTelar(ByVal CodigoTipo As String) As DataTable

            Dim dtVelocidadTelar As DataTable
            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"CodigoTipoMaquina", CodigoTipo}
                dtVelocidadTelar = accesoDatos.ObtenerDataTable("GET_VELOCIDAD_TELAR", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

            Return dtVelocidadTelar

        End Function

        Public Sub grabarLineaProgramacionTejeduria()

            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"Fecha", fecha, "CodigoPlanta", codigoPlanta, _
                            "CodigoTipoPlanta", codigoTipoMaquina, "CodigoArticulo", codigoArticulo, _
                            "RevisionArticulo", revisionArticulo, "Velocidad_teorica", Velocidad_Teorica, _
                            "Cantidad", cantidad, "Dias", dias, _
                            "MetrosTejer", metrosTejer, "NumeroTelas", numeroTelas, "UsuarioCreacion", usuario}
                accesoDatos.EjecutarComando("NM_INS_PROGRAMACION_TELARES", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Sub eliminarLineaProgramacionTejeduria()

            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"Fecha", fecha, _
                                                "CodigoArticulo", codigoArticulo, _
                                                "RevisionArticulo", revisionArticulo, _
                                                "CodigoTipoMaquina", codigoTipoMaquina}
                accesoDatos.EjecutarComando("NM_DEL_PROGRAMACION_TELARES", objParametros)
            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Sub actualizarLineaProgramacionTejeduria()

            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objparametros As Object() = {"Fecha", fecha, "CodigoPlanta", codigoPlanta, _
                            "CodigoTipoMaquina", codigoTipoMaquina, "CodigoTipoNuevo", codigoTipoNuevo, _
                            "CodigoArticulo", codigoArticulo, "RevisionArticulo", revisionArticulo, _
                            "Velocidad_teorica", Velocidad_Teorica, "Cantidad", cantidad, "Dias", dias, _
                            "MetrosTejer", metrosTejer, "NumeroTelas", numeroTelas, _
                            "UsuarioModificacion", usuario}
                accesoDatos.EjecutarComando("NM_UPD_PROGRAMACION_TELARES", objparametros)
            Catch ex As Exception
                Throw ex
            End Try
        End Sub
        Public Function ListarProgramas(ByVal pstr_Fecha_Desde As String, ByVal pstr_Fecha_Hasta As String, ByVal pstr_Estado As String) As DataTable


            Dim dtbPrograma As DataTable
            Try
                Dim accesoDatos As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Produccion)
                Dim objParametros As Object() = {"pvch_Fecha_Desde", pstr_Fecha_Desde, "pvch_Fecha_Hasta", pstr_Fecha_Hasta, "vch_Estado", pstr_Estado}
                dtbPrograma = accesoDatos.ObtenerDataTable("usp_tel_programatelares_listaestado_valorizacion", objParametros)

            Catch ex As Exception
                Throw ex
            End Try

            Return dtbPrograma

        End Function
#End Region

    End Class
End Namespace
