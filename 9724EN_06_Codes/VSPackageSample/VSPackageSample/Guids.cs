// Guids.cs
// MUST match guids.h
using System;

namespace PacktPub.VSPackageSample
{
    static class GuidList
    {
        public const string guidVSPackageSamplePkgString = "1c032716-fa57-4004-9092-6549fbc78c2b";
        public const string guidVSPackageSampleCmdSetString = "453f3081-349d-4ed9-b92b-8fe7dc903d12";
        public const string guidToolWindowPersistanceString = "0cec96a6-d903-4abe-9304-1378cd9bc523";
        public const string guidVSPackageSampleEditorFactoryString = "256983e2-0dc5-4b8f-8cc5-2563f5aa163a";

        public static readonly Guid guidVSPackageSampleCmdSet = new Guid(guidVSPackageSampleCmdSetString);
        public static readonly Guid guidVSPackageSampleEditorFactory = new Guid(guidVSPackageSampleEditorFactoryString);
    };
}