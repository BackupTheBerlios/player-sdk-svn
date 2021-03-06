#region Copyright
//
// Nini Configuration Project.
// Copyright (C) 2004 Brent R. Matzelle.  All rights reserved.
//
// This software is published under the terms of the MIT X11 license, a copy of 
// which has been included with this distribution in the LICENSE.txt file.
// 
#endregion

using System.Reflection;
using System.Runtime.CompilerServices;

#if (NET_1_0)
[assembly: AssemblyTitle("Nini for .NET Framework 1.0")]
#elif (NET_1_1)
[assembly: AssemblyTitle("Nini for .NET Framework 1.1")]
#elif (MONO_1_0)
[assembly: AssemblyTitle("Nini for Mono 1.0")]
#else
[assembly: AssemblyTitle("Nini")]
#endif

[assembly: AssemblyDescription("Configuration Library - http://nini.sourceforge.net/")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Brent R. Matzelle")]
[assembly: AssemblyProduct("Nini")]
[assembly: AssemblyCopyright("Copyright (c) 2004 Brent R. Matzelle. All Rights Reserved.")]
[assembly: AssemblyTrademark("Copyright (c) 2004 Brent R. Matzelle. All Rights Reserved.")]
[assembly: AssemblyDefaultAlias("Nini")]
[assembly: AssemblyCulture("")]

#if STRONG
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile(@"..\..\Nini.snk")]
#endif

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly: System.Reflection.AssemblyVersion("0.8.0.0")]

