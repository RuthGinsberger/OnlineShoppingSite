namespace Dal.DO;
public struct Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerAdress { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? ShipDate { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public override string ToString() => $@" Order ID: {ID},
    Customer name: {CustomerName}, Customer email : {CustomerEmail}, Customer adress: {CustomerAdress}, Order date: {OrderDate},
    Ship date: {ShipDate}, Delivery date : {DeliveryDate}";
}

