using System.ComponentModel.DataAnnotations;

namespace FirmarOnline.Model.PSC
{
    /// <summary>
    /// Tipo de evento
    /// </summary>
    public enum AuditEventType
    {
        /// <summary>
        /// Ninguno
        /// </summary>
        [Display(Name = "Ninguno")]
        None = 0,

        #region Eventos a nivel de sobre

        /// <summary>
        /// Sobre creado
        /// </summary>
        [Display(Name = "Sobre creado")]
        DocumentSetCreated = 100,

        /// <summary>
        /// Sobre en proceso
        /// </summary>
        [Display(Name = "Sobre en proceso")]
        DocumentSetInProcess = 101,

        /// <summary>
        /// Sobre completado
        /// </summary>
        [Display(Name = "Sobre completado")]
        DocumentSetCompleted = 102,

        /// <summary>
        /// Sobre expirado
        /// </summary>
        [Display(Name = "Sobre expirado")]
        DocumentSetExpired = 103,

        /// <summary>
        /// Sobre cancelado
        /// </summary>
        [Display(Name = "Sobre cancelado")]
        DocumentSetCanceled = 104,

        /// <summary>
        /// Sobre eliminado
        /// </summary>
        [Display(Name = "Sobre eliminado")]
        DocumentSetDeleted = 105,

        /// <summary>
        /// Sobre rechazado
        /// </summary>
        [Display(Name = "Sobre rechazado")]
        DocumentSetRejected = 106,

        /// <summary>
        /// Enviado recordatorio
        /// </summary>
        [Display(Name = "Enviado recordatorio")]
        ReminderSent = 107, // Evento para envio recordatorio

        /// <summary>
        /// Sobre en estado erróneo
        /// </summary>
        [Display(Name = "Sobre en estado erróneo")]
        DocumentSetError = 108,

        /// <summary>
        /// Purgado manual
        /// </summary>
        [Display(Name = "Purgado manual")]
        DocumentSetPurgeManual = 110,

        #endregion Eventos a nivel de sobre

        #region Eventos a nivel de destinatario

        /// <summary>
        /// Destinatario en proceso
        /// </summary>
        [Display(Name = "Destinatario en proceso")]
        RecipientInProcess = 201,

        /// <summary>
        /// Acciones de destinatario completadas
        /// </summary>
        [Display(Name = "Acciones de destinatario completadas")]
        RecipientCompleted = 202,

        /// <summary>
        /// Destinatario activo y pendiente de firma
        /// </summary>
        [Display(Name = "Destinatario activo")]
        RecipientActivated = 203,

        /// <summary>
        /// Se ha obtenido el link de acceso al sobre mediante llamada API.
        /// </summary>
        [Display(Name = "URL de acceso al sobre obtenida mediante llamada API")]
        RecipientGetUrl = 204,

        #endregion Eventos a nivel de destinatario

        #region Eventos a nivel de documento

        /// <summary>
        /// Documento abierto
        /// </summary>
        [Display(Name = "Documento abierto")]
        DocumentOpened = 302,

        /// <summary>
        /// Página leída
        /// </summary>
        [Display(Name = "Página leída")]
        PageRead = 303,

        /// <summary>
        /// Documento leído
        /// </summary>
        [Display(Name = "Documento leído")]
        DocumentRead = 304,

        /// <summary>
        /// Documento aceptado
        /// </summary>
        [Display(Name = "Documento aceptado")]
        DocumentAccepted = 305,

        /// <summary>
        /// Documento notificado
        /// </summary>
        [Display(Name = "Documento notificado")]
        DocumentNotified = 306,

        /// <summary>
        /// Documento firmado
        /// </summary>
        [Display(Name = "Documento firmado")]
        DocumentSigned = 307,

        /// <summary>
        /// Documento rechazado
        /// </summary>
        [Display(Name = "Documento rechazado")]
        DocumentRejected = 308,

        /// <summary>
        /// Documento resellado
        /// </summary>
        [Display(Name = "Documento resellado")]
        DocumentTimestamped = 309,

        /// <summary>
        /// Otp Validado
        /// </summary>
        [Display(Name = "Otp Validado")]
        OtpPinValidated = 310,

        /// <summary>
        /// Otp no validado
        /// </summary>
        [Display(Name = "Otp no validado")]
        OtpPinNotValidated = 311,

        /// <summary>
        /// Firmar bio recibida
        /// </summary>
        [Display(Name = "Firmar bio recibida")]
        BioSignReceived = 312,

        /// <summary>
        /// Aceptación de términos y condiciones
        /// </summary>
        [Display(Name = "Aceptación de términos y condiciones")]
        TermsAndConditionsAccepted = 314,

        /// <summary>
        /// Autenticación por otp válida
        /// </summary>
        [Display(Name = "Autenticación por otp válida")]
        AuthenticationOtpValidated = 315,

        /// <summary>
        /// Autenticación por otp inválida
        /// </summary>
        [Display(Name = "Autenticación por otp inválida")]
        AuthenticationOtpNotValidated = 316,

        /// <summary>
        /// Autenticación por código de acceso válida
        /// </summary>
        [Display(Name = "Autenticación por código de acceso válida")]
        AuthenticationAccessCodeValidated = 317,

        /// <summary>
        /// Autenticación por código de acceso inválida
        /// </summary>
        [Display(Name = "Autenticación por código de acceso inválida")]
        AuthenticationAccessCodeNotValidated = 318,

        /// <summary>
        /// Firma corporativa de documento
        /// </summary>
        [Display(Name = "Firma corporativa de documento")]
        DocumentCorporateSignature = 319,

        /// <summary>
        /// Autenticación mediante MRZ válida
        /// </summary>
        [Display(Name = "Autenticación mediante MRZ válida")]
        AuthenticationMRZValidated = 320,

        /// <summary>
        /// Autenticación mediante MRZ inválida
        /// </summary>
        [Display(Name = "Autenticación mediante MRZ inválida")]
        AuthenticationMRZNotValidated = 321,

        /// <summary>
        /// Certificado, clave privada o password incorrecto del certificado PKCS #8
        /// </summary>
        [Display(Name = "Certificado PKCS#8 incorrecto")]
        PKCS8CertificateNotValidated = 322,

        /// <summary>
        /// Autenticación por OTP WhatsApp válida
        /// </summary>
        [Display(Name = "Autenticación por OTP WhatsApp válida")]
        AuthenticationOtpWhatsAppValidated = 323,

        /// <summary>
        /// Autenticación por OTP WhatsApp inválida
        /// </summary>
        [Display(Name = "Autenticación por OTP WhatsApp inválida")]
        AuthenticationOtpWhatsAppNotValidated = 324,

        /// <summary>
        /// Documento descargado.
        /// </summary>
        [Display(Name = "Documento descargado.")]
        DocumentDownloaded = 325,

        /// <summary>
        /// Anexo.
        /// </summary>
        [Display(Name = "Anexo.")]
        Attachment = 326,

        // Notificaciones
        /// <summary>
        /// Inicio del proceso mail a proveedor
        /// </summary>
        [Display(Name = "Inicio del proceso mail a proveedor ")]
        StartProcessMail = 399,

        /// <summary>
        /// Correo enviado
        /// Message has been successfully delivered to the receiving server.
        /// </summary>
        [Display(Name = "Correo enviado")]
        DeliveredMail = 400,

        /// <summary>
        /// Message has been received and is ready to be delivered.
        /// </summary>
        [Display(Name = "Correo procesado")]
        ProcessedMail = 401,

        /// <summary>
        /// Recipient has opened the HTML message. Open Tracking needs to be enabled for this type of event.
        /// </summary>
        [Display(Name = "Correo abierto")]
        OpenMail = 402,

        /// <summary>
        /// Recipient clicked on a link within the message. Click Tracking needs to be enabled for this type of event.
        /// </summary>
        [Display(Name = "Click dentro del correo")]
        ClickInsideMail = 403,

        /// <summary>
        /// You may see the following drop reasons: Invalid SMTPAPI header, Spam Content (if Spam Checker app is enabled), Unsubscribed Address, Bounced Address, Spam Reporting Address, Invalid, Recipient List over Package Quota
        /// </summary>
        [Display(Name = "Correo caido")]
        DroppedMail = 404,

        /// <summary>
        /// Receiving server temporarily rejected the message
        /// </summary>
        [Display(Name = "Correo diferido")]
        DeferredMail = 405,

        /// <summary>
        /// Receiving server could not or would not accept mail to this recipient permanently. If a recipient has previously unsubscribed from your emails, the message is dropped.
        /// </summary>
        [Display(Name = "Correo de rebote")]
        BounceMail = 406,

        /// <summary>
        /// Receiving server could not or would not accept the message temporarily. If a recipient has previously unsubscribed from your emails, the message is dropped.
        /// </summary>
        [Display(Name = "Correo bloqueado")]
        BlockedMail = 407,

        /// <summary>
        /// Recipient marked message as spam.
        /// </summary>
        [Display(Name = "Correo marcado como spam")]
        SpamReportMail = 408,

        // Eventos Sms
        /// <summary>
        /// Inicio del proceso sms a proveedor
        /// </summary>
        [Display(Name = "Inicio del proceso sms")]
        StartProcessSms = 499,

        /// <summary>
        /// Sms enviado
        /// </summary>
        [Display(Name = "Sms enviado")]
        DeliveredSms = 500,

        /// <summary>
        /// Sms desconocido
        /// </summary>
        [Display(Name = "Sms desconocido")]
        UnknownSms = 501,

        /// <summary>
        /// Sms pendiente
        /// </summary>
        [Display(Name = "Sms pendiente")]
        PendingSms = 502,

        /// <summary>
        /// Sms expirado
        /// </summary>
        [Display(Name = "Sms expirado")]
        ExpiredSms = 503,

        /// <summary>
        /// Sms no entregado
        /// </summary>
        [Display(Name = "Sms no entregado")]
        UndeliveredSms = 504,

        /// <summary>
        /// Sms rechazado
        /// </summary>
        [Display(Name = "Sms rechazado")]
        RejectedSms = 505,

        // Eventos WhatApp
        /// <summary>
        /// WhatApp enviado
        /// </summary>
        [Display(Name = "WhatApp enviado")]
        SentWhatsApp = 599,

        /// <summary>
        /// WhatApp entregado
        /// </summary>
        [Display(Name = "WhatApp entregado")]
        DeliveredWhatsApp = 600,

        /// <summary>
        /// WhatApp leido
        /// </summary>
        [Display(Name = "WhatApp leido")]
        ReadWhatsApp = 601,

        /// <summary>
        /// WhatApp no enviado por error
        /// </summary>
        [Display(Name = "WhatApp no enviado por error")]
        FailedWhatsApp = 602,

        /// <summary>
        /// WhatApp no entregado
        /// </summary>
        [Display(Name = "WhatApp no entregado")]
        UndeliveredWhatsApp = 603,

        /// <summary>
        /// WhatApp error desconocido
        /// </summary>
        [Display(Name = "WhatApp error desconocido")]
        UnknownWhatsApp = 604,

        // Envio documentos firmados por método Post
        /// <summary>
        /// Documento firmado enviado por método Post correctamente
        /// </summary>
        [Display(Name = "Documento firmado enviado por método Post correctamente")]
        SendDocumentPost = 699,

        /// <summary>
        /// Documento firmado enviado por método Post correctamente con errores
        /// </summary>
        [Display(Name = "Documento firmado enviado por método Post correctamente con errores")]
        SendDocumentPostError = 700,

        #endregion Eventos a nivel de documento
    }
}