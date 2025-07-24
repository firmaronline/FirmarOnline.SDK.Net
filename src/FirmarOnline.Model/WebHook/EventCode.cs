using System;

namespace FirmarOnline.Model.WebHook
{
    /// <summary>
    /// Listado de eventos
    /// </summary>
    public enum EventCode
    {
        /// <summary>
        /// No definido
        /// </summary>
        none = 0,

        // PSC
        // eventos documentset / envelop / sobre
        /// <summary>
        /// Sobre creado
        /// </summary>
        PSCDocumentSetCreated = 100,

        /// <summary>
        /// Sobre en curso
        /// </summary>
        PSCDocumentSetInProcess = 101,

        /// <summary>
        /// Sobre completado correctamente
        /// </summary>
        PSCDocumentSetCompleted = 102,

        /// <summary>
        /// Sobre caducado
        /// </summary>
        PSCDocumentSetExpired = 103,

        /// <summary>
        /// Sobre cancelado
        /// </summary>
        PSCDocumentSetCanceled = 104,

        /// <summary>
        /// Sobre borrado
        /// </summary>
        PSCDocumentSetDeleted = 105,

        /// <summary>
        /// Sobre rechazado
        /// </summary>
        PSCDocumentSetRejected = 106,

        /// <summary>
        /// Recordatorio enviado
        /// </summary>
        PSCDocumentSetReminderSent = 107,

        /// <summary>
        /// Sobre pasa a estado error
        /// </summary>
        PSCDocumentSetError = 108,

        /// <summary>
        /// Sobre se ha recuperado de un error
        /// </summary>
        [Obsolete("EventCode.PSCDocumentSetRecoveredError value is obsolete and will be removed in a future release.")]
        PSCDocumentSetRecoveredError = 109,

        // Eventos Recipient / Persona
        /// <summary>
        /// Persona creada
        /// </summary>
        [Obsolete("EventCode.PSCRecipientCreated value is obsolete and will be removed in a future release.")]
        PSCRecipientCreated = 200,

        /// <summary>
        /// Persona en curso
        /// </summary>
        PSCRecipientInProcess = 201,

        /// <summary>
        /// Persona finalizada
        /// </summary>
        PSCRecipientCompleted = 202,

        /// <summary>
        /// Persona activada.
        /// </summary>
        PSCRecipientActivated = 203, /*OJO*/ // Este evento antes era rechazo de destinatario. Está cambiado porque parecía que estaba mal, pero no estoy seguro al 100%.

        /// <summary>
        /// Persona cancela
        /// </summary>
        [Obsolete("EventCode.PSCDocumentCanceled value is obsolete and will be removed in a future release. Use " + nameof(EventCode.PSCDocumentSetCanceled) + " instead.")]
        PSCRecipientCanceled = 205,

        // Eventos RecipientAction / Pareja persona-Documento
        /// <summary>
        /// RecipientAction creado
        /// </summary>
        [Obsolete("EventCode.PSCRecipientActionCreated value is obsolete and will be removed in a future release. Use " + nameof(EventCode.PSCDocumentSetCreated) + " instead.")]
        PSCRecipientActionCreated = 300,

        /// <summary>
        /// RecipientAction en proceso
        /// </summary>
        [Obsolete("EventCode.PSCRecipientActionInProcess value is obsolete and will be removed in a future release. Use " + nameof(EventCode.PSCRecipientInProcess) + " instead.")]
        PSCRecipientActionInProcess = 301,

        // Eventos Visor Documentos
        /// <summary>
        /// Documento abierto
        /// </summary>
        PSCDocumentOpened = 302,

        /// <summary>
        /// Página leida de un documento
        /// </summary>
        PSCPageRead = 303,

        /// <summary>
        /// Documento leído
        /// </summary>
        PSCDocumentRead = 304,

        /// <summary>
        /// Documento Aceptado
        /// </summary>
        PSCDocumentAccepted = 305,

        /// <summary>
        /// Documento notificado (recipientAction type = 10)
        /// </summary>
        PSCDocumentNotified = 306,

        /// <summary>
        /// Documento firmado
        /// </summary>
        PSCDocumentSigned = 307,

        /// <summary>
        /// Documento rechazado
        /// </summary>
        [Obsolete("EventCode.PSCDocumentRejected value is obsolete and will be removed in a future release. Use " + nameof(EventCode.PSCDocumentSetRejected) + " instead.")]
        PSCDocumentRejected = 308,

        /// <summary>
        /// Documento resellado
        /// </summary>
        PSCDocumentTimestamped = 309,

        /// <summary>
        /// Otp validado
        /// </summary>
        PSCOtpPinValidated = 310,

        /// <summary>
        /// Otp no validado
        /// </summary>
        PSCOtpPinNotValidated = 311,

        /// <summary>
        /// Recibida firma bio
        /// </summary>
        PSCBioSignReceived = 312,

        /// <summary>
        /// Aceptación de términos y condiciones
        /// </summary>
        PSCTermsAndConditionsAccepted = 314,

        /// <summary>
        /// Autenticación por otp válida
        /// </summary>
        PSCAuthenticationOtpValidated = 315,

        /// <summary>
        /// Autenticación por otp inválida
        /// </summary>
        PSCAuthenticationOtpNotValidated = 316,

        /// <summary>
        /// Autenticación código acceso válida
        /// </summary>
        PSCAuthenticationAccessCodeValidated = 317,

        /// <summary>
        /// Autenticación código acceso inválido
        /// </summary>
        PSCAuthenticationAccessCodeNotValidated = 318,

        /// <summary>
        /// Firma corporativa de documento
        /// </summary>
        PSCDocumentCorporateSignature = 319,

        /// <summary>
        /// RecipientAction cancelado
        /// </summary>
        [Obsolete("EventCode.PSCDocumentCanceled value is obsolete and will be removed in a future release. Use " + nameof(EventCode.PSCDocumentSetCanceled) + " instead.")]
        PSCDocumentCanceled = 360,

        // Eventos mail
        /// <summary>
        /// Inicio del proceso mail a proveedor
        /// </summary>
        PSCStartProcessMail = 399,

        /// <summary>
        /// Correo enviado
        /// </summary>
        PSCDeliveredMail = 400,

        /// <summary>
        /// Correo procesado
        /// </summary>
        PSCProcessedMail = 401,

        /// <summary>
        /// Correo abierto
        /// </summary>
        PSCOpenMail = 402,

        /// <summary>
        /// Click en correo
        /// </summary>
        PSCClickInsideMail = 403,

        // Eventos Sms
        /// <summary>
        /// Inicio del proceso sms a proveedor
        /// </summary>
        PSCStartProcessSms = 499,

        /// <summary>
        /// Sms enviado
        /// </summary>
        PSCDeliveredSms = 500,

        /// <summary>
        /// Sms desconocido
        /// </summary>
        PSCUnknownSms = 501,

        /// <summary>
        /// Sms pendiente
        /// </summary>
        PSCPendingSms = 502,

        /// <summary>
        /// Sms expirado
        /// </summary>
        PSCExpiredSms = 503,

        /// <summary>
        /// Sms no entregado
        /// </summary>
        PSCUndeliveredSms = 504,

        /// <summary>
        /// Sms rechazado
        /// </summary>
        PSCRejectedSms = 505,

        // SIGNFROMAPP
        // Eventos lado "Izquierdo"
        /// <summary>
        /// Nuevo documento para dispositivo
        /// </summary>
        SignFromAppNewDocument = 600,

        /// <summary>
        /// Obtener información documento
        /// </summary>
        SignFromAppGetDocument = 620,

        /// <summary>
        /// Borrado documento previo envío al dispositivo
        /// </summary>
        SignFromAppDeleteDocument = 630,

        /// <summary>
        /// Obtener estado del documento
        /// </summary>
        SignFromAppGetDocumentState = 640,

        /// <summary>
        /// Obtener listado de histórico trazabilidad
        /// </summary>
        SignFromAppGetDocumentTrazability = 650,

        // Eventos lado "derecho"
        /// <summary>
        /// El dispositivo solicita el documento más antiguo para firmar
        /// </summary>
        DeviceGetDocument = 700,

        /// <summary>
        /// El dispositivo solicita todos los documentos para firmar
        /// </summary>
        DeviceGetAllDocuments = 710,

        /// <summary>
        /// El dispositivo envía un documento firmado
        /// </summary>
        DeviceNewSignedDocument = 720,

        /// <summary>
        /// el dispositivo rechaza un documento
        /// </summary>
        DeviceRejectedDocument = 730,

        /// <summary>
        /// el dispositivo elimina un documento
        /// </summary>
        DeviceDeletedDocument = 740,

        /// <summary>
        /// el dispositivo notifica un error en el documento
        /// </summary>
        DeviceDocumentError = 750,

        /// <summary>
        /// el dispositivo solicita cambiar el estado de un documento
        /// </summary>
        DeviceDocumentStateChanged = 760,

        // Eventos WhatApp
        /// <summary>
        /// WhatApp enviado
        /// </summary>
        PSCSentWhatsApp = 799,

        /// <summary>
        /// WhatApp entregado
        /// </summary>
        PSCDeliveredWhatsApp = 800,

        /// <summary>
        /// WhatApp leido
        /// </summary>
        PSCReadWhatsApp = 801,

        /// <summary>
        /// WhatApp no enviado por error
        /// </summary>
        PSCFailedWhatsApp = 802,

        /// <summary>
        /// WhatApp no entregado
        /// </summary>
        PSCUndeliveredWhatsApp = 803,

        /// <summary>
        /// WhatApp error desconocido
        /// </summary>
        PSCUnknownWhatsApp = 804,

        // PSC (BulkDownload)
        /// <summary>
        /// Descarga masiva de documentos creada
        /// </summary>
        PSCBulkDownloadCreated = 1000,

        /// <summary>
        /// Descarga masiva de documentos en proceso
        /// </summary>
        PSCBulkDownloadInProcess = 1001,

        /// <summary>
        /// Descarga masiva de documentos completada correctamente
        /// </summary>
        PSCBulkDownloadCompleted = 1002,

        /// <summary>
        /// Descarga masiva de documentos en error
        /// </summary>
        PSCBulkDownloadError = 1003,
    }
}