using Certification.AzureFundamentals.Shared;

namespace Certification.AzureFundamentals.CloudComputing;

public static class Section01_WhatIsCloud
{
    public static void Run()
    {
        Console.WriteLine("Cloud computing: delivery of computing services (servers, storage, networking, databases, software, analytics, AI)");
        Console.WriteLine("OVER THE INTERNET, ON DEMAND, and billed ONLY FOR WHAT YOU USE.\n");

        Console.WriteLine("The five essential characteristics (NIST):");
        Table.Print(["Characteristic", "Meaning", "Example in Azure"],
        [
            ["On-demand self-service", "You provision resources yourself, without asking anyone", "A VM in 2 minutes via portal/CLI"],
            ["Broad network access", "Access over the network from any device", "Portal, REST API, mobile app"],
            ["Resource pooling", "Physical resources are shared between customers", "Multi-tenant datacenters"],
            ["Rapid elasticity", "Capacity grows/shrinks quickly (automatically)", "Autoscale, VM Scale Sets"],
            ["Measured service", "Usage is metered and you pay accordingly", "Per-second/hour/transaction billing"],
        ]);

        Console.WriteLine("\nComparison with a traditional datacenter (the need for a new web server):");
        Table.Print(["", "Your own datacenter", "Cloud"],
        [
            ["Time to provision a resource", "Weeks (purchasing, installation)", "Minutes"],
            ["Upfront investment", "Hardware + licenses (CapEx)", "None — pay as you go (OpEx)"],
            ["Capacity planning", "Guess for the peak; the surplus is wasted", "Just what you need; scale up/down"],
            ["Maintenance, power, physical security", "Yours", "The provider's"],
            ["Global reach", "Needs a new datacenter", "Minutes in another region"],
        ]);
    }
}
