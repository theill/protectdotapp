# protectdotapp

This service helps .NET developers of commercial software to protect their code from decompilation.

## Problem

Developers doing commercial software on .NET will need to obfuscate their code before sending to customers. This is needed because it’s very easy to decompile .NET applications.

Shareware developers face a challenge when they need to either use a obfuscator because they need to carefully consider what, where and how to obfuscate their code. It’s time a developer would rather use doing actual development.

Semi-automatic solutions exists but they are expensive typically around $2000.

## Solution

We propose a different solution to this problem. We protect a given .NET application for a small fee allowing developers to focus on building great applications. Each time a developer wants to release a new version of their product, they use our service.

### Web Site

It should be possible for developers to protect their application easily without having to do any configuration.

+ Developer visits http://protectdotapp.com
+ Developer uploads .zip or .msi file of their compiled product
+ + We scan files for .NET assemblies
+ + We generate unique PIN for developer
+ Developer sends an SMS with PIN to a given shortcode
+ + We resolve PIN from developer
+ + We generate updated .zip or .msi
+ Developer downloads secured .zip or .msi package

### API (v2)

Developers must be able to automate process so it’s possible to incorporate into their own build process. Using our API it must be possible to get a protected stream back e.g.
Developer initiaties a POST to http://api.protectdotapp.com/protect/<token>
Body includes .zip/.msi
We respond with a 200 OK
Content-type: application/octet-stream
Body includes protected .zip/.msi

In order for this to work we need to support prepaid accounts e.g. it should be possible for customers to register a developer account and transfer money to it, before using our API.

### Web Site for Developers (v2)

+ Developers using our API needs to register for an account.
+ Developer clicks “Sign up”
+ Developer enters their “Email” and “Password”
+ Developer is shown a “transfer money to your account” screen
+ Developer transfers money to our PayPal account
+ + We store transferred amount
+ + We generate a developer token
+ + We generate a MSBUILD task (todo)
+ Developer is shown token and usage information

It's now possible for developer to use this token to call our API.

### Technical Solution

Application is wrapped in native Win32 application and all .NET assemblies are encrypted inside. Once started, the .NET assemblies are decrypted and loaded dynamically.
(todo: michael, how do we do this properly?)

### System Requirements

In order for this system to work we need (brainstorming)

+ VPS with Windows (done)
+ ZayPay (SMS payment gateway) agreement (done)
+ PayPal Business Account (done) (v2)


