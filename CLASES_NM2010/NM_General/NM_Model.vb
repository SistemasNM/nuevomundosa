Imports System.ComponentModel
Imports System.Collections
Imports System.Threading

Namespace NM_Model
    Public MustInherit Class NM_Pool

        Private _busyObjects As New Hashtable()
        Private _freeObjects As New Hashtable()

        Protected MustOverride Function Validate(ByVal obj As Object) As Boolean

        Protected MustOverride Function Create() As Object

        Protected MustOverride Sub Expire(ByVal obj As Object)

        Protected Sub New()

        End Sub

        Protected Sub CheckIn(ByVal obj As Object)

            SyncLock (_busyObjects)
                _busyObjects.Remove(obj)
            End SyncLock

            SyncLock (_freeObjects)
                _freeObjects.Add(Now.TimeOfDay.TotalMilliseconds, obj)
            End SyncLock

        End Sub

        Protected Function CheckOut(ByVal expiryMilliSeconds As Double) As Object

            SyncLock (_freeObjects)

                If _freeObjects.Count > 0 Then

                    Dim en As IDictionaryEnumerator = _freeObjects.GetEnumerator

                    While en.MoveNext

                        If (Now.TimeOfDay.TotalMilliseconds - CDbl(en.Key) > expiryMilliSeconds) Then

                            If Validate(en.Entry) Then
                                Dim o As Object = en.Value

                                _freeObjects.Remove(en.Key)

                                SyncLock (_busyObjects)
                                    _busyObjects.Add(o, o)
                                End SyncLock

                                Return o
                            Else
                                Expire(en.Value)

                                _freeObjects.Remove(en.Key)

                                en = _freeObjects.GetEnumerator
                            End If
                        Else
                            Expire(en.Value)

                            _freeObjects.Remove(en.Key)

                            en = _freeObjects.GetEnumerator
                        End If

                    End While
                End If

                Dim obj As Object = Create()

                SyncLock (_busyObjects)
                    _busyObjects.Add(obj, obj)
                End SyncLock

                Return obj

            End SyncLock

        End Function

        Protected Function Count() As Integer

            Return _freeObjects.Count + _busyObjects.Count

        End Function

        Protected Function CountCheckedIn() As Integer

            Return _freeObjects.Count

        End Function

        Protected Function CountCheckedOut() As Integer

            Return _busyObjects.Count()

        End Function
    End Class


End Namespace