Imports NM.AccesoDatos
Imports NM_General.NM_BaseDatos

Namespace NM_Hilanderia
    Public Class NM_InventarioPabilera

        Public Codigo_Inventario As String
        Public Codigo_Maquina As String
        Public revision_maquina As Integer
        Public Codigo_MateriaPrima As String
        Public Ne_Nominal As Double
        Public Tachos_Maquina As Double
        Public Avance_Tachos As Double
        Public Pabilos_Maquina As Double
        Public Avance_Pabilos As Double
        Public Pabilos_Casillero As Double
        Public Usuario As String
        Public Codigo_Tacho As String


        Sub New()
            Codigo_Inventario = ""
            Codigo_Maquina = ""
            revision_maquina = 0
            Codigo_MateriaPrima = ""
            Ne_Nominal = 0
            Tachos_Maquina = 0
            Avance_Tachos = 0
            Pabilos_Maquina = 0
            Avance_Pabilos = 0
            Pabilos_Casillero = 0
            Usuario = ""
            Codigo_Tacho = ""
        End Sub

        Function Add() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", Codigo_Inventario, _
                                                  "codigo_maquina", Codigo_Maquina, _
                                                  "revision_maquina", revision_maquina, _
                                                  "codigo_materia_prima", Codigo_MateriaPrima, _
                                                  "Ne_nominal", Ne_Nominal, _
                                                  "tachos_maquina", Tachos_Maquina, _
                                                  "avance_tachos", Avance_Tachos, _
                                                  "pabilos_maquina", Pabilos_Maquina, _
                                                  "avance_pabilos", Avance_Pabilos, _
                                                  "pabilos_casillero", Pabilos_Casillero, _
                                                  "Usuario", Usuario, _
                                                   "codigo_tacho", Codigo_Tacho, _
                                                  "int_accion", 1}

                lobjConexion.EjecutarComando("usp_hil_InvPabilera_actualizar", lobjParametros)
                Add = True
            Catch
                Add = False
            End Try

        End Function

        Function Update() As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", Codigo_Inventario, _
                                                  "codigo_maquina", Codigo_Maquina, _
                                                  "revision_maquina", revision_maquina, _
                                                  "codigo_materia_prima", Codigo_MateriaPrima, _
                                                  "Ne_nominal", Ne_Nominal, _
                                                  "tachos_maquina", Tachos_Maquina, _
                                                  "avance_tachos", Avance_Tachos, _
                                                  "pabilos_maquina", Pabilos_Maquina, _
                                                  "avance_pabilos", Avance_Pabilos, _
                                                  "pabilos_casillero", Pabilos_Casillero, _
                                                  "Usuario", Usuario, _
                                                   "codigo_tacho", Codigo_Tacho, _
                                                  "int_accion", 2}

                lobjConexion.EjecutarComando("usp_hil_InvPabilera_actualizar", lobjParametros)
                Update = True
            Catch
                Update = False
            End Try
        End Function

        Function Delete(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal sCodigoMateria As String) As Boolean
            Try
                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "codigo_materia_prima", sCodigoMateria}

                lobjConexion.EjecutarComando("usp_hil_InvPabilera_eliminar", lobjParametros)
                Delete = True
            Catch
                Delete = False
            End Try

        End Function

        Function List() As DataTable
            Try

                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", ""}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvPabilera_listar", lobjParametros)
                Return dt
            Catch
            End Try

        End Function

        Function List(ByVal Fecha As Date) As DataTable
            Try

                Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
                Dim dt As New DataTable

                Dim lobjParametros As Object() = {"fecha", Format(Fecha, "yyyy/MM/dd")}
                dt = lobjConexion.ObtenerDataTable("usp_hil_InvPabilera_listar", lobjParametros)
                Return dt
            Catch
            End Try

        End Function


        Function Exist(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal sCodigoMateria As String) As Boolean

            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim dt As New DataTable

            Try

                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "codigo_materia_prima", sCodigoMateria}

                dt = lobjConexion.ObtenerDataTable("usp_hil_InvPabilera_obtener", lobjParametros)

                Return (dt.Rows.Count > 0)
            Catch
                Return False
            End Try

        End Function

        Sub Seek(ByVal sCodigoInventario As String, ByVal sCodigoMaquina As String, ByVal sCodigoMateria As String)

            Dim fila As DataRow

            Dim lobjConexion As New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)
            Dim dt As New DataTable

            Try


                Dim lobjParametros As Object() = {"codigo_inventario", sCodigoInventario, _
                                                  "codigo_maquina", sCodigoMaquina, _
                                                  "codigo_materia_prima", sCodigoMateria}

                dt = lobjConexion.ObtenerDataTable("usp_hil_InvPabilera_obtener", lobjParametros)

                For Each fila In dt.Rows
                    If IsDBNull(fila("ne_nominal")) = False Then Ne_Nominal = fila("ne_nominal")
                    If IsDBNull(fila("tachos_maquina")) = False Then Tachos_Maquina = fila("tachos_maquina")
                    If IsDBNull(fila("avance_tachos")) = False Then Avance_Tachos = fila("avance_tachos")
                    If IsDBNull(fila("pabilos_maquina")) = False Then Pabilos_Maquina = fila("pabilos_maquina")
                    If IsDBNull(fila("avance_pabilos")) = False Then Avance_Pabilos = fila("avance_pabilos")
                    If IsDBNull(fila("pabilos_casillero")) = False Then Pabilos_Casillero = fila("pabilos_casillero")
                    If IsDBNull(fila("codigo_tacho")) = False Then Codigo_Tacho = fila("codigo_tacho")

                    Codigo_Inventario = sCodigoInventario
                    Codigo_Maquina = sCodigoMaquina
                    Codigo_MateriaPrima = sCodigoMateria

                Next

            Catch ex As Exception

            End Try
        End Sub

    End Class

End Namespace