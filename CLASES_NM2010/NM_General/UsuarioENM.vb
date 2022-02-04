Public Class UsuarioENM
#Region "    Variables"
    Private mstrID As String
    Private mobjTrabajador As OFISIS.OFIPLAN.Trabajador
    Private mstrUsuario As String
#End Region
#Region "   Propiedades"
    Public Property ID() As String
        Get
            ID = mstrID
        End Get
        Set(ByVal Value As String)
            mstrID = Value
        End Set
    End Property
    Public Property Trabajador() As OFISIS.OFIPLAN.Trabajador
        Get
            Trabajador = mobjTrabajador
        End Get
        Set(ByVal Value As OFISIS.OFIPLAN.Trabajador)
            mobjTrabajador = Value
        End Set
    End Property
#End Region
#Region "Metodos"
    Public Function Buscar()
        Dim lobjProd As NM.AccesoDatos.AccesoDatosSQLServer
        Dim lstrParametros() As String = {"var_ID", mstrID}
        Dim ldtRes As DataTable
        Try
            lobjProd = New NM.AccesoDatos.AccesoDatosSQLServer(NM.AccesoDatos.GeneradorCadenaConexion.enmBasesDatos.Produccion)
            ldtRes = lobjProd.ObtenerDataTable("usp_ADM_Usuario_Trabajador", lstrParametros)
            If ldtRes.Rows.Count = 1 Then
                With ldtRes.Rows(0)
                    mobjTrabajador = New OFISIS.OFIPLAN.Trabajador
                    mobjTrabajador.Codigo = .Item("codigo_trabajador")
                    mobjTrabajador.ApellidoPaterno = .Item("apellido_paterno")
                    mobjTrabajador.ApellidoMaterno = .Item("apellido_materno")
                    mobjTrabajador.Nombres = .Item("nombres")
                    mobjTrabajador.Activo = IIf(.Item("estado") = "ACT", True, False)
                End With
            Else
                mobjTrabajador = New OFISIS.OFIPLAN.Trabajador
            End If
        Catch ex As Exception
            mobjTrabajador = New OFISIS.OFIPLAN.Trabajador
        Finally
            lobjProd = Nothing
        End Try
    End Function
#End Region
End Class
