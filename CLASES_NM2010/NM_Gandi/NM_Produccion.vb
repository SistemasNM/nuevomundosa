'Option Strict On

Imports System.Data
Imports System.Xml
Imports System.IO
Imports NM.AccesoDatos

Namespace NMGandi

    Public Class NM_Produccion
        Implements IDisposable

#Region " Declaracion de Propiedades Publicas "

        Private i_anno As String
        Private i_periodo As String
        Private i_planta As String
        Private i_puesto As String
        Private i_codigo_trabajador As String
        Private i_horas_trabajadas As String
        Private i_valor_importe As Double
        Private i_valor_eficiencia As Double
        Private i_valor_filas As Integer
        Private i_bonificacion_telar As Double
        Private i_bonificacion_continua As Double
        Private i_jornal As Double

        Public Property iAnno() As String
            Get
                Return i_anno
            End Get
            Set(ByVal Value As String)
                i_anno = Value
            End Set
        End Property

        Public Property iPeriodo() As String
            Get
                Return i_periodo
            End Get
            Set(ByVal Value As String)
                i_periodo = Value
            End Set
        End Property

        Public Property iPlanta() As String
            Get
                Return i_planta
            End Get
            Set(ByVal Value As String)
                i_planta = Value
            End Set
        End Property

        Public Property iPuesto() As String
            Get
                Return i_puesto
            End Get
            Set(ByVal Value As String)
                i_puesto = Value
            End Set
        End Property

        Public Property icodigo_trabajador() As String
            Get
                Return i_codigo_trabajador
            End Get
            Set(ByVal Value As String)
                i_codigo_trabajador = Value
            End Set
        End Property

        Public Property ihoras_trabajadas() As String
            Get
                Return i_horas_trabajadas
            End Get
            Set(ByVal Value As String)
                i_horas_trabajadas = Value
            End Set
        End Property

        Public Property ivalor_importe() As Double
            Get
                Return i_valor_importe
            End Get
            Set(ByVal Value As Double)
                i_valor_importe = Value
            End Set
        End Property

        Public Property ivalor_eficiencia() As Double
            Get
                Return i_valor_eficiencia
            End Get
            Set(ByVal Value As Double)
                i_valor_eficiencia = Value
            End Set
        End Property

        Public Property ivalor_filas() As Integer
            Get
                Return i_valor_filas
            End Get
            Set(ByVal Value As Integer)
                i_valor_filas = Value
            End Set
        End Property

        Public Property ibonificacion_telar() As Double
            Get
                Return i_bonificacion_telar
            End Get
            Set(ByVal Value As Double)
                i_bonificacion_telar = Value
            End Set
        End Property


        Public Property ibonificacion_continua() As Double
            Get
                Return i_bonificacion_continua
            End Get
            Set(ByVal Value As Double)
                i_bonificacion_continua = Value
            End Set
        End Property

        Public Property ijornal() As Double
            Get
                Return i_jornal
            End Get
            Set(ByVal Value As Double)
                i_jornal = Value
            End Set
        End Property

#End Region

#Region " Declaracion de Variables Miembro "
        Private m_sqlDtAccGandi As AccesoDatosSQLServer
#End Region

#Region " Definicion de Constructores "
        Sub New()
            m_sqlDtAccGandi = New AccesoDatosSQLServer(GeneradorCadenaConexion.enmBasesDatos.Gandi)
        End Sub
#End Region

#Region " Definicion de Metodos "

        Function Exist(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pCodigo_Trabajador As String, ByVal pFlag As String) As Boolean
            Dim dtblDatos As DataTable
            Dim objParametros() As Object = {"intANNO", pAnno, "intPERIODO", pPeriodo, "varPLANTA", pPlanta, "varPUESTO", pPuesto, "varTRABAJADOR", pCodigo_Trabajador, "flag", pFlag}
            dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_CALC_GD_INDEPENDIENTE", objParametros)

            i_anno = pAnno
            i_periodo = pPeriodo
            i_codigo_trabajador = pCodigo_Trabajador
            i_planta = pPlanta
            i_puesto = pPuesto

            If dtblDatos.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Function List(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pPlanta As String, ByVal pPuesto As String, ByVal pCodigo_Trabajador As String, ByVal pFlag As String) As DataTable
            Dim dtblDatos As DataTable
            Dim drwDatos As DataRow
            Dim objSUMfilas As Object
            Dim objSUMimporte As Object
            Dim objSUMeficiencia As Object

            Dim objParametros() As Object = {"intANNO", pAnno, "intPERIODO", pPeriodo, "varPLANTA", pPlanta, "varPUESTO", pPuesto, "varTRABAJADOR", pCodigo_Trabajador, "flag", pFlag}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_CALC_GD_INDEPENDIENTE", objParametros)

                objSUMfilas = dtblDatos.Compute("Count(eficiencia)", "eficiencia > 0")
                objSUMimporte = dtblDatos.Compute("Sum(importe_base)", "importe_base > 0")
                i_valor_filas = Format(CInt(objSUMfilas), "0")
                i_valor_importe = Format(CDbl(objSUMimporte), "0.000")
                i_horas_trabajadas = dtblDatos.Rows(0).Item("horas_trabajadas")

                If pPlanta = "150" Then
                    objSUMeficiencia = dtblDatos.Compute("Sum(eficiencia)", "eficiencia > 0")
                    i_valor_eficiencia = Format(CDbl(objSUMeficiencia), "0.000")
                End If

                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Bonificacion_Telar(ByVal pPuesto As String, ByVal pEficiencia As Double) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"varPUESTO", pPuesto, "promedio", pEficiencia}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_BONIFICACION_TELAR", objParametros)
                i_bonificacion_telar = dtblDatos.Rows(0).Item("bonificacion")
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Function Jornal(ByVal pCodigo_Trabajador As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"VARcodigoTrabajador", pCodigo_Trabajador}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_Calculo_Jornal", objParametros)
                i_jornal = dtblDatos.Rows(0).Item("jornal")
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function


        Function Bonificacion_Continua(ByVal pAnno As String, ByVal pPeriodo As String, ByVal pCodigo_Trabajador As String) As DataTable
            Dim dtblDatos As DataTable

            Dim objParametros() As Object = {"ANNO", pAnno, "PERIODO", pPeriodo, "CODIGO_TRABAJADOR", pCodigo_Trabajador}
            Try
                dtblDatos = m_sqlDtAccGandi.ObtenerDataTable("P_NM_BONIFICACION_CONTINUA", objParametros)
                i_bonificacion_continua = dtblDatos.Rows(0).Item("bonificacion")
                Return dtblDatos

            Catch ex As Exception
                Throw ex
            End Try
        End Function

#End Region

        Public Sub Dispose() Implements System.IDisposable.Dispose
            m_sqlDtAccGandi.Dispose()
        End Sub
    End Class

End Namespace
