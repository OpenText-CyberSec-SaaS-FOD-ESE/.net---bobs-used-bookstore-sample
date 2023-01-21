using Amazon.CDK;
using Constructs;
using Amazon.CDK.AWS.EC2;

namespace SharedInfrastructure.Production;

public class NetworkStack : Stack
{
    public Vpc Vpc { get; private set; }

    internal NetworkStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
    {
        // Create a new vpc spanning two AZs and with public and private subnets
        // to host the application resources
        Vpc = new Vpc(this, "BobsBookstoreVpc", new VpcProps
        {
            IpAddresses = IpAddresses.Cidr("10.0.0.0/16"),
            // Cap at 2 AZs in case we are deployed to a region with only 2
            MaxAzs = 2,
            // We do not need a NAT gateway for this sample application, so remove to reduce cost
            NatGateways = 1,
            SubnetConfiguration = new[]
            {
                new SubnetConfiguration
                {
                    CidrMask = 24,
                    SubnetType = SubnetType.PUBLIC,
                    Name = "BookstorePublicSubnet"
                },
                new SubnetConfiguration
                {
                    CidrMask = 24,
                    SubnetType = SubnetType.PRIVATE_WITH_EGRESS,
                    Name = "BookstorePrivateSubnet"
                }
            }
        });
    }
}