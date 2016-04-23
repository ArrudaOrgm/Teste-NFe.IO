using ADODB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TesteNFe
{
    public class Teste
    {

        public static ImpostoRetidoBO Impostos(float Valordafatura, Connection cn) 
        {

            var RsTabelaTaxas = new ADODB.Recordset();
            var Chamada = new ImpostoRetidoBO();

            try 
            {
                //Pega as taxas cadastradas da tabela "TabelaTaxas"
                RsTabelaTaxas.Open(string.Format("SELECT * FROM TabelaTaxas"),cn,CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockReadOnly);

                    if (!RsTabelaTaxas.EOF) 
                    {
                        //Faz algumas verificações no valor da Fatura, faz o arredondamento para duas casas Decimais após a virgula e preenche o Objeto com os Totais.
                        if (Valordafatura * (double)RsTabelaTaxas.Fields["TaxaIR_Retido"].Value > 10)
                            Chamada.IR_Retido = Math.Round((Valordafatura * (double)RsTabelaTaxas.Fields["TaxaIR_Retido"].Value),2);
                        if (Valordafatura > 5000)
                            Chamada.PIS_Retido = Math.Round((Valordafatura * (double)RsTabelaTaxas.Fields["TaxaPIS_Retido"].Value),2);
                            Chamada.Cofins_Retido = Math.Round((Valordafatura * (double)RsTabelaTaxas.Fields["TaxaCOFINS_Retido"].Value), 2);
                            Chamada.CSLL_Retido = Math.Round((Valordafatura * (double)RsTabelaTaxas.Fields["TaxaCSLL_Retido"].Value),2); 
                    }else{
                        throw new Exception("Cadastre as Taxas dos impostos");
                    } 
                RsTabelaTaxas.Close();


                return Chamada; 


            }catch(Exception){
                throw;
            }

        }




    }
}
