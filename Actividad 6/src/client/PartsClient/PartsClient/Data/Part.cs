using System;
using System.Collections.Generic;

namespace PartsClient.Data;

[Serializable]
public class Part
{
    public string PartID { get; set; }
    public string PartName { get; set; }
    public string TheSuppliers { get; set; }
    public string PartType { get; set; }
    public List<string> Suppliers { get; set; } = new List<string>();
    public DateTime PartAvailableDate { get; set; }

    public string SupplierString
    {
        get
        {
            return string.Join(", ", Suppliers);
        }
    }
}
