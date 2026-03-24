Module Module1

    'Declaração de Variáveis Públicas
    Public diretorio As String
    Public db As New ADODB.Connection
    Public rs As New ADODB.Recordset
    Public sql As String
    Public cont As Integer

    Sub Carregar_tipo()

        Try

            sql = "SELECT DISTINCT Categoria FROM venda_carros ORDER BY Categoria"
            rs = db.Execute(sql)

            With Form1.cmb_categoria

                .Items.Clear()

                Do While Not rs.EOF

                    .Items.Add(rs.Fields("Categoria").Value)
                    rs.MoveNext()

                Loop

            End With

        Catch ex As Exception

            MsgBox("Erro ao carregar categorias", MsgBoxStyle.Critical, "Erro")

        End Try

    End Sub


    Sub Carregar_dados()

        Try
            sql = "SELECT * FROM venda_carros ORDER BY IdProduto ASC"
            rs = db.Execute(sql)

            With Form1.dgv_dados
                cont = 0
                .Rows.Clear()

                Do While Not rs.EOF
                    cont += 1

                    'Pega preços
                    Dim precoCusto As Double = CDbl(rs.Fields("PrecoCusto").Value)
                    Dim precoVenda As Double = CDbl(rs.Fields("PrecoVenda").Value)

                    'Calcula margem
                    Dim margem As Double = 0
                    If precoVenda > 0 Then
                        margem = ((precoVenda - precoCusto) / precoVenda) * 100
                    End If

                    'Adiciona linha ao DataGridView
                    .Rows.Add(cont,
                          rs.Fields("DescricaoProduto").Value,
                          rs.Fields("DataLote").Value,
                          rs.Fields("Categoria").Value,
                          rs.Fields("MarcaFabricante").Value,
                          rs.Fields("PrecoVenda").Value,
                          margem.ToString("F2") & " %") 'Mostra com 2 casas decimais

                    rs.MoveNext()
                Loop
            End With

        Catch ex As Exception
            MsgBox("Erro ao carregar dados: " & ex.Message, MsgBoxStyle.Critical, "ERRO")
        End Try

    End Sub


    Sub Conecta_banco()

        Try

            db = CreateObject("ADODB.Connection")

            db.Open("Provider=SQLOLEDB;" &
                    "Data Source=LAB5-16;" &
                    "Initial Catalog=cad_3dsm26;" &
                    "trusted_connection=yes;")

            MsgBox("Conexão OK", MsgBoxStyle.Information, "AVISO")

        Catch ex As Exception

            MsgBox("Erro ao conectar", MsgBoxStyle.Critical, "AVISO")

        End Try

    End Sub


    Sub Limpar_cadastro()

        Try
            With Form1
                .txt_desc.Clear()
                .cmb_data_lote.Value = Now

                .cmb_categoria.SelectedIndex = 0
                .cmb_marca.SelectedIndex = -1


                .qtd_disponivel.Value = 0
                .text_custo.Clear()
                .cmb_venda.Clear()

                .img_foto.Load(Application.StartupPath & "\icones\Elegantthemes-Beautiful-Flat-One-Color-Car.128.png")

                .txt_desc.Focus()
            End With
        Catch ex As Exception
            MsgBox("Erro ao limpar formulário: " & ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub

End Module