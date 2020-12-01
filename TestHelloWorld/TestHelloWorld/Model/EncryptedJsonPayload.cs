using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestHelloWorld.Model
{

    public class EncryptedJsonPayload
    {
        public Datapdu DataPDU { get; set; }
    }

    public class Datapdu
    {
        public string Revision { get; set; }
        public Body Body { get; set; }
        public string _xmlns { get; set; }
    }

    //public class Body
    //{
    //    public Apphdr AppHdr { get; set; }
    //    public Document Document { get; set; }
    //}

    //public class Apphdr
    //{
    //    public Fr Fr { get; set; }
    //    public To To { get; set; }
    //    public string BizMsgIdr { get; set; }
    //    public string MsgDefIdr { get; set; }
    //    public string BizSvc { get; set; }
    //    public DateTime CreDt { get; set; }
    //    public Signature Signature { get; set; }
    //    public Security Security { get; set; }
    //    public string _xmlns { get; set; }
    //}

    //public class Fr
    //{
    //    public Fiid FIId { get; set; }
    //}

    //public class Fiid
    //{
    //    public Fininstnid FinInstnId { get; set; }
    //}

    //public class Fininstnid
    //{
    //    public string BICFI { get; set; }
    //}

    //public class To
    //{
    //    public Fiid1 FIId { get; set; }
    //}

    //public class Fiid1
    //{
    //    public Fininstnid1 FinInstnId { get; set; }
    //}

    //public class Fininstnid1
    //{
    //    public string BICFI { get; set; }
    //}

    //public class Signature
    //{
    //    public Signaturemethod SignatureMethod { get; set; }
    //    public Reference Reference { get; set; }
    //    public Signaturevalue SignatureValue { get; set; }
    //    public string _xmlnsds { get; set; }
    //    public string __prefix { get; set; }
    //    public string __text { get; set; }
    //}

    //public class Signaturemethod
    //{
    //    public string _Algorithm { get; set; }
    //    public string __prefix { get; set; }
    //}

    //public class Reference
    //{
    //    public Digestmethod DigestMethod { get; set; }
    //    public Digestvalue DigestValue { get; set; }
    //    public string __prefix { get; set; }
    //}

    //public class Digestmethod
    //{
    //    public string _Algorithm { get; set; }
    //    public string __prefix { get; set; }
    //}

    //public class Digestvalue
    //{
    //    public string __prefix { get; set; }
    //    public string __text { get; set; }
    //}

    //public class Signaturevalue
    //{
    //    public string __prefix { get; set; }
    //    public string __text { get; set; }
    //}

    //public class Security
    //{
    //    public Aes Aes { get; set; }
    //    public string _xmlnsidtp { get; set; }
    //    public string __prefix { get; set; }
    //}

    //public class Aes
    //{
    //    public string _key { get; set; }
    //    public string _iv { get; set; }
    //    public string __prefix { get; set; }
    //}

    //public class Document
    //{
    //    public Fitoficstmrcdttrf FIToFICstmrCdtTrf { get; set; }
    //    public string _xmlns { get; set; }
    //}

    //public class Fitoficstmrcdttrf
    //{
    //    public Grphdr GrpHdr { get; set; }
    //    public Cdttrftxinf CdtTrfTxInf { get; set; }
    //    public Splmtrydata SplmtryData { get; set; }
    //}

    //public class Grphdr
    //{
    //    public string MsgId { get; set; }
    //    public DateTime CreDtTm { get; set; }
    //    public string NbOfTxs { get; set; }
    //    public string TtlIntrBkSttlmAmt { get; set; }
    //    public string IntrBkSttlmDt { get; set; }
    //    public Sttlminf SttlmInf { get; set; }
    //}

    //public class Sttlminf
    //{
    //    public string SttlmMtd { get; set; }
    //}

    //public class Cdttrftxinf
    //{
    //    public Pmtid PmtId { get; set; }
    //    public Pmttpinf PmtTpInf { get; set; }
    //    public Intrbksttlmamt IntrBkSttlmAmt { get; set; }
    //    public string ChrgBr { get; set; }
    //    public Instgagt InstgAgt { get; set; }
    //    public Instdagt InstdAgt { get; set; }
    //    public Dbtr Dbtr { get; set; }
    //    public Dbtracct DbtrAcct { get; set; }
    //    public Dbtragt DbtrAgt { get; set; }
    //    public Dbtragtacct DbtrAgtAcct { get; set; }
    //    public Cdtragt CdtrAgt { get; set; }
    //    public Cdtragtacct CdtrAgtAcct { get; set; }
    //    public Cdtr Cdtr { get; set; }
    //    public Cdtracct CdtrAcct { get; set; }
    //    public Rmtinf RmtInf { get; set; }
    //}

    //public class Pmtid
    //{
    //    public string InstrId { get; set; }
    //    public string EndToEndId { get; set; }
    //    public string TxId { get; set; }
    //}

    //public class Pmttpinf
    //{
    //    public string ClrChanl { get; set; }
    //    public Svclvl SvcLvl { get; set; }
    //    public Lclinstrm LclInstrm { get; set; }
    //    public Ctgypurp CtgyPurp { get; set; }
    //}

    //public class Svclvl
    //{
    //    public string Prtry { get; set; }
    //}

    //public class Lclinstrm
    //{
    //    public string Prtry { get; set; }
    //}

    //public class Ctgypurp
    //{
    //    public string Prtry { get; set; }
    //}

    //public class Intrbksttlmamt
    //{
    //    public string _Ccy { get; set; }
    //    public string __text { get; set; }
    //}

    //public class Instgagt
    //{
    //    public Fininstnid2 FinInstnId { get; set; }
    //}

    //public class Fininstnid2
    //{
    //    public string BICFI { get; set; }
    //}

    //public class Instdagt
    //{
    //    public Fininstnid3 FinInstnId { get; set; }
    //}

    //public class Fininstnid3
    //{
    //    public string BICFI { get; set; }
    //}

    //public class Dbtr
    //{
    //    public string Nm { get; set; }
    //}

    //public class Dbtracct
    //{
    //    public Id Id { get; set; }
    //}

    //public class Id
    //{
    //    public Othr Othr { get; set; }
    //}

    //public class Othr
    //{
    //    public string Id { get; set; }
    //}

    //public class Dbtragt
    //{
    //    public Fininstnid4 FinInstnId { get; set; }
    //    public Brnchid BrnchId { get; set; }
    //}

    //public class Fininstnid4
    //{
    //    public string BICFI { get; set; }
    //}

    //public class Brnchid
    //{
    //    public string Id { get; set; }
    //}

    //public class Dbtragtacct
    //{
    //    public Id1 Id { get; set; }
    //}

    //public class Id1
    //{
    //    public Othr1 Othr { get; set; }
    //}

    //public class Othr1
    //{
    //    public string Id { get; set; }
    //}

    //public class Cdtragt
    //{
    //    public Fininstnid5 FinInstnId { get; set; }
    //    public Brnchid1 BrnchId { get; set; }
    //}

    //public class Fininstnid5
    //{
    //    public string BICFI { get; set; }
    //}

    //public class Brnchid1
    //{
    //    public string Id { get; set; }
    //}

    //public class Cdtragtacct
    //{
    //    public Id2 Id { get; set; }
    //}

    //public class Id2
    //{
    //    public Othr2 Othr { get; set; }
    //}

    //public class Othr2
    //{
    //    public string Id { get; set; }
    //}

    //public class Cdtr
    //{
    //    public string Nm { get; set; }
    //}

    //public class Cdtracct
    //{
    //    public Id3 Id { get; set; }
    //}

    //public class Id3
    //{
    //    public Othr3 Othr { get; set; }
    //}

    //public class Othr3
    //{
    //    public string Id { get; set; }
    //}

    //public class Rmtinf
    //{
    //    public string Ustrd { get; set; }
    //}

    //public class Splmtrydata
    //{
    //    public string PlcAndNm { get; set; }
    //    public Envlp Envlp { get; set; }
    //}

    //public class Envlp
    //{
    //    public Tx_Tracking_Info Tx_Tracking_Info { get; set; }
    //}

    //public class Tx_Tracking_Info
    //{
    //    public string RefNo_SendingPSP { get; set; }
    //    public string RefNo_SendingBank { get; set; }
    //    public string RefNo_ReceivingBank { get; set; }
    //    public string RefNo_ReceivingPSP { get; set; }
    //    public string RefNo_IDTP { get; set; }
    //}


}
