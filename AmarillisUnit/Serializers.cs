namespace AmarillisUnit
{
    class Location
    {
        public string title { get; set; }
        public string location_type { get; set; }
        public int woeid { get; set; }
        public string latt_long { get; set; }
        new public string ToString() { return $"{this.title} {this.location_type} {this.woeid} {this.latt_long}"; }
    }
}