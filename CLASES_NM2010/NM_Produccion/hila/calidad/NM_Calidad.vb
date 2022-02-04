Imports NM_General.NM_BaseDatos
Imports NM.AccesoDatos
Public Class NM_Calidad
    Private m_objConnection As AccesoDatosSQLServer
    Sub New()
        m_objConnection = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Hilanderia)

    End Sub

    Public Function ListarTitulo() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaTitulo")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
	End Function
	Public Function ListarTitulo(ByVal pstrCodigoEmpresa As String) As DataTable
		Dim dtblDatos As DataTable
		Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
		Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ObtenerTitulosPruebas", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
		Return dtblDatos
	End Function

	Public Function ListarCodigohilo(ByVal pstrCodigoEmpresa As String) As DataTable
		Dim dtblDatos As DataTable
		Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
		Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ObtenerHilosPruebas", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
		Return dtblDatos
	End Function
	Public Function ListarCodigohilo() As DataTable
		Dim dtblDatos As DataTable
		Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaCodigoHilo")
		Catch ex As Exception
			Throw ex
		End Try
		Return dtblDatos
	End Function

	Public Function ListarMaquina(ByVal pstrCodigoEmpresa As String) As DataTable
		Dim dtblDatos As DataTable
		Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
		Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ObtenerMaquinasPruebas", objParametros)
		Catch ex As Exception
			Throw ex
		End Try
		Return dtblDatos
	End Function
	Public Function ListarMaquina() As DataTable
		Dim dtblDatos As DataTable
		Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaMaquina")
		Catch ex As Exception
			Throw ex
		End Try
		Return dtblDatos
	End Function
	Public Function ListarTituloEmpalme() As DataTable
		Dim dtblDatos As DataTable
		Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaTituloEmpalme")
		Catch ex As Exception
			Throw ex
		End Try
		Return dtblDatos
    End Function
    Public Function ListarTituloEmpalme(ByVal pstrCodigoEmpresa As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_qry_ListaTituloEmpalme", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarCodigohiloEmpalme() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaCodigoHiloEmpalme")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarCodigohiloEmpalme(ByVal pstrCodigoEmpresa As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_qry_ListaCodigoHiloEmpalme", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarMaquinaEmpalme() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaMaquinaEmpalme")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarMaquinaEmpalme(ByVal pstrCodigoEmpresa As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_qry_ListaMaquinasEmpalme", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarTituloEmpalmeDI() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaTituloEmpalmeDI")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarTituloEmpalmeDI(ByVal pstrCodigoEmpresa As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_qry_ListaTituloEmpalmeDI", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarCodigohiloEmpalmeDI() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaCodigoHiloEmpalmeDI")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarCodigohiloEmpalmeDI(ByVal pstrCodigoEmpresa As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_qry_ListaCodigoHiloDI", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarMaquinaEmpalmeDI() As DataTable
        Dim dtblDatos As DataTable
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_ListaMaquinaEmpalmeDI")
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
    Public Function ListarMaquinaEmpalmeDI(ByVal pstrCodigoEmpresa As String) As DataTable
        Dim dtblDatos As DataTable
        Dim objParametros() As Object = {"p_var_CodigoEmpresa", pstrCodigoEmpresa}
        Try
            dtblDatos = m_objConnection.ObtenerDataTable("usp_CAL_qry_ListaMaquinasDI", objParametros)
        Catch ex As Exception
            Throw ex
        End Try
        Return dtblDatos
    End Function
End Class
