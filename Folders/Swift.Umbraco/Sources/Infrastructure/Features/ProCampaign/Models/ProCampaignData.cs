using Swift.Umbraco.Models.Enum;
using Newtonsoft.Json;
using System;
using System.Dynamic;

namespace $safeprojectname$.$safeprojectname$.ProCampaign.Models
{
    public class ProCampaignData
    {
        public static string FormatParticipationData(CrmData data)
        {
       
            var participation = (Countries)data.Data.Country == Countries.IE ?
                new
                {
                    Source = data.GetSetting<string>("SourceName"),
                    Attributes = new[]
                    {
                        new
                        {
                            Name = "Firstname",
                            Value = data.Data.Firstname
                        },
                        new
                        {
                            Name = "Lastname",
                            Value = data.Data.Lastname
                        },
                        new
                        {
                            Name = "Email",
                            Value = data.Data.Email
                        },
                        new
                        {
                            Name = "MobilePrivate",
                            Value = data.Data.MobilePrivate
                        },
                        new
                        {
                            Name = "Street1",
                            Value = data.Data.Street1
                        },
                        new
                        {
                            Name = "Street2",
                            Value = data.Data.Street2
                        },
                        new
                        {
                            Name = "City",
                            Value = data.Data.City
                        },
                        new
                        {
                            Name = "Zipcode",
                            Value = data.Data.ZipCode
                        },
                        new
                        {
                            Name = "Country",
                            Value = data.Data.Country.ToString().ToUpper()
                        },
                        new
                        {
                            Name = "TBZZ190101_Accountnumber",
                            Value = data.Data.AccountNumber
                        },
                        new
                        {
                            Name = "TBZZ190101_Sortcode",
                            Value = data.Data.SortCode
                        },
                        new
                        {
                            Name = "TBZZ190101_IBAN",
                            Value = data.Data.IBAN
                        },
                        new
                        {
                            Name = "TBZZ190101_BIC",
                            Value = data.Data.BIC
                        },
                        new
                        {
                            Name = "List:Privacy_Policy_EN",
                            Value = (object)1
                        },
                        new
                        {
                            Name = "List:TBZZ190101_Participants",
                            Value = (object)1
                        }
                    },
                    Transactions = new[]
                    {
                        new
                        {
                            Name = data.GetSetting<string>("TransactionName"),
                            Date_Created = DateTime.UtcNow.ToString(),
                            Parameters = new []
                            {
                                new
                                {
                                    Name = "Ident_short",
                                    Value = (PaymentType)data.Data.Payment == PaymentType.CHEQUE ?
                                                (dynamic)"Cheque" : (dynamic)"Bank Transfer"
                                },
                                new
                                {
                                    Name = "Value",
                                    Value = data.Data.PrizeValue
                                },
                                new
                                {
                                    Name = "Q1",
                                    Value = data.Data.AccountNumber
                                },
                                new
                                {
                                    Name = "Q2",
                                    Value = data.Data.SortCode
                                },
                                new
                                {
                                    Name = "Q3",
                                    Value = data.Data.IBAN
                                },
                                new
                                {
                                    Name = "Q4",
                                    Value = data.Data.BIC
                                },
                                new
                                {
                                    Name = "Q15",
                                    Value = data.Data.ParticipationId
                                }
                            }
                        }
                    },
                    LegalTextVersions = new[]
                    {
                        new
                        {
                            LegalTextName = data.Data.PrivacyPolicyTextName,
                            Version = data.Data.PrivacyPolicyVersion,
                            Created = data.Data.PrivacyPolicyCreation
                        }
                    }
                } :
                new
                {
                    Source = data.GetSetting<string>("SourceName"),
                    Attributes = new[]
                    {
                        new
                        {
                            Name = "Firstname",
                            Value = data.Data.Firstname
                        },
                        new
                        {
                            Name = "Lastname",
                            Value = data.Data.Lastname
                        },
                        new
                        {
                            Name = "Email",
                            Value = data.Data.Email
                        },
                        new
                        {
                            Name = "MobilePrivate",
                            Value = data.Data.MobilePrivate
                        },
                        new
                        {
                            Name = "Street1",
                            Value = data.Data.Street1
                        },
                        new
                        {
                            Name = "Street2",
                            Value = data.Data.Street2
                        },
                        new
                        {
                            Name = "City",
                            Value = data.Data.City
                        },
                        new
                        {
                            Name = "Zipcode",
                            Value = data.Data.ZipCode
                        },
                        new
                        {
                            Name = "Country",
                            Value = (object)"GB"
                        },
                        new
                        {
                            Name = "TBZZ190101_Accountnumber",
                            Value = data.Data.AccountNumber
                        },
                        new
                        {
                            Name = "TBZZ190101_Sortcode",
                            Value = data.Data.SortCode
                        },
                        new
                        {
                            Name = "List:Privacy_Policy_EN",
                            Value = (object)1
                        },
                        new
                        {
                            Name = "List:TBZZ190101_Participants",
                            Value = (object)1
                        }
                    },
                    Transactions = new[]
                    {
                        new
                        {
                            Name = data.GetSetting<string>("TransactionName"),
                            Date_Created = DateTime.UtcNow.ToString(),
                            Parameters = new []
                            {
                                new
                                {
                                    Name = "Ident_short",
                                    Value = (PaymentType)data.Data.Payment == PaymentType.CHEQUE ?
                                                (dynamic)"Cheque" : (dynamic)"Bank Transfer"
                                },
                                new
                                {
                                    Name = "Value",
                                    Value = data.Data.PrizeValue
                                },
                                new
                                {
                                    Name = "Q1",
                                    Value = data.Data.AccountNumber
                                },
                                new
                                {
                                    Name = "Q2",
                                    Value = data.Data.SortCode
                                },
                                new
                                {
                                    Name = "Q15",
                                    Value = data.Data.ParticipationId
                                }
                            }
                        }
                    },
                    LegalTextVersions = new[]
                    {
                        new
                        {
                            LegalTextName = data.Data.PrivacyPolicyTextName,
                            Version = data.Data.PrivacyPolicyVersion,
                            Created = data.Data.PrivacyPolicyCreation
                        }
                    }
                };

            return JsonConvert.SerializeObject(participation);
        }
    }
}
