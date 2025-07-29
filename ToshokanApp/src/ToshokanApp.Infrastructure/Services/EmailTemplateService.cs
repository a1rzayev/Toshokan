using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class EmailTemplateService : IEmailTemplateService
{
    public string GenerateVerificationEmailTemplate(string verificationCode, string userName)
    {
        return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Email Verification - Toshokan</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }}
        
        .email-container {{
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 40px rgba(0,0,0,0.1);
            overflow: hidden;
            max-width: 600px;
            width: 100%;
        }}
        
        .header {{
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 40px 30px;
            text-align: center;
        }}
        
        .header h1 {{
            font-size: 28px;
            font-weight: 600;
            margin-bottom: 10px;
        }}
        
        .header p {{
            font-size: 16px;
            opacity: 0.9;
        }}
        
        .content {{
            padding: 40px 30px;
        }}
        
        .greeting {{
            font-size: 18px;
            color: #333;
            margin-bottom: 30px;
            line-height: 1.6;
        }}
        
        .verification-box {{
            background: linear-gradient(135deg, #f093fb 0%, #f5576c 100%);
            border-radius: 20px;
            padding: 40px;
            text-align: center;
            margin: 40px 0;
            box-shadow: 0 15px 40px rgba(240, 147, 251, 0.4);
            border: 3px solid rgba(255, 255, 255, 0.2);
        }}
        
        .verification-code {{
            font-size: 72px;
            font-weight: 900;
            color: white;
            letter-spacing: 16px;
            margin: 40px 0;
            font-family: 'Courier New', monospace;
            text-shadow: 4px 4px 8px rgba(0,0,0,0.5);
            text-align: center;
            display: block;
            line-height: 1.2;
        }}
        
        .instructions {{
            color: #666;
            font-size: 16px;
            line-height: 1.6;
            margin-bottom: 30px;
        }}
        
        .footer {{
            background: #f8f9fa;
            padding: 30px;
            text-align: center;
            border-top: 1px solid #eee;
        }}
        
        .footer p {{
            color: #666;
            font-size: 14px;
            margin-bottom: 10px;
        }}
        
        .logo {{
            font-size: 24px;
            font-weight: bold;
            color: white;
            margin-bottom: 10px;
        }}
        
        .security-note {{
            background: #fff3cd;
            border: 1px solid #ffeaa7;
            border-radius: 8px;
            padding: 15px;
            margin: 20px 0;
            color: #856404;
            font-size: 14px;
        }}
        
        @media (max-width: 600px) {{
            .email-container {{
                margin: 10px;
                border-radius: 15px;
            }}
            
            .header, .content, .footer {{
                padding: 20px;
            }}
            
            .verification-code {{
                font-size: 48px;
                letter-spacing: 8px;
                font-weight: 900;
            }}
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <div class=""logo"">📚 Toshokan</div>
            <h1>Email Verification</h1>
            <p>Complete your account verification</p>
        </div>
        
        <div class=""content"">
            <div class=""greeting"">
                Hello <strong>{userName}</strong>,<br>
                Thank you for joining Toshokan! To complete your account setup, please verify your email address.
            </div>
            
            <div class=""verification-box"">
                <h3 style=""color: white; margin-bottom: 30px; font-size: 24px; font-weight: 600;"">Your Verification Code</h3>
                <div class=""verification-code"">{verificationCode}</div>
                <p style=""color: white; font-size: 16px; opacity: 0.9; margin-top: 20px;"">Enter this code on the verification page</p>
            </div>
            
            <div class=""instructions"">
                <strong>How to verify your email:</strong><br>
                1. Copy the verification code above<br>
                2. Go back to your Toshokan account<br>
                3. Paste the code in the verification field<br>
                4. Click ""Verify"" to complete the process
            </div>
            
            <div class=""security-note"">
                <strong>🔒 Security Note:</strong> This code will expire in 10 minutes. If you didn't request this verification, please ignore this email.
            </div>
        </div>
        
        <div class=""footer"">
            <p><strong>Toshokan - Your Digital Library</strong></p>
            <p>Discover, read, and explore thousands of books</p>
            <p style=""margin-top: 20px; font-size: 12px; color: #999;"">
                This email was sent to verify your account. Please do not reply to this email.
            </p>
        </div>
    </div>
</body>
</html>";
    }

    public string GenerateWelcomeEmailTemplate(string userName)
    {
        return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Welcome to Toshokan</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}
        
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }}
        
        .email-container {{
            background: white;
            border-radius: 20px;
            box-shadow: 0 20px 40px rgba(0,0,0,0.1);
            overflow: hidden;
            max-width: 600px;
            width: 100%;
        }}
        
        .header {{
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 40px 30px;
            text-align: center;
        }}
        
        .header h1 {{
            font-size: 28px;
            font-weight: 600;
            margin-bottom: 10px;
        }}
        
        .content {{
            padding: 40px 30px;
        }}
        
        .welcome-message {{
            font-size: 18px;
            color: #333;
            margin-bottom: 30px;
            line-height: 1.6;
        }}
        
        .features {{
            background: #f8f9fa;
            border-radius: 15px;
            padding: 30px;
            margin: 30px 0;
        }}
        
        .feature-item {{
            display: flex;
            align-items: center;
            margin-bottom: 20px;
            color: #333;
        }}
        
        .feature-icon {{
            font-size: 24px;
            margin-right: 15px;
            width: 40px;
            text-align: center;
        }}
        
        .footer {{
            background: #f8f9fa;
            padding: 30px;
            text-align: center;
            border-top: 1px solid #eee;
        }}
        
        .logo {{
            font-size: 24px;
            font-weight: bold;
            color: white;
            margin-bottom: 10px;
        }}
        
        @media (max-width: 600px) {{
            .email-container {{
                margin: 10px;
                border-radius: 15px;
            }}
            
            .header, .content, .footer {{
                padding: 20px;
            }}
        }}
    </style>
</head>
<body>
    <div class=""email-container"">
        <div class=""header"">
            <div class=""logo"">📚 Toshokan</div>
            <h1>Welcome to Toshokan!</h1>
            <p>Your journey to knowledge begins here</p>
        </div>
        
        <div class=""content"">
            <div class=""welcome-message"">
                Hello <strong>{userName}</strong>,<br><br>
                Welcome to Toshokan! We're excited to have you join our community of book lovers and knowledge seekers.
            </div>
            
            <div class=""features"">
                <h3 style=""margin-bottom: 20px; color: #333;"">What you can do with Toshokan:</h3>
                
                <div class=""feature-item"">
                    <div class=""feature-icon"">📖</div>
                    <div>
                        <strong>Discover Books:</strong> Browse through thousands of books across all genres
                    </div>
                </div>
                
                <div class=""feature-item"">
                    <div class=""feature-icon"">💳</div>
                    <div>
                        <strong>Purchase Books:</strong> Buy and download your favorite books instantly
                    </div>
                </div>
                
                <div class=""feature-item"">
                    <div class=""feature-icon"">❤️</div>
                    <div>
                        <strong>Wishlist:</strong> Save books you want to read later
                    </div>
                </div>
                
                <div class=""feature-item"">
                    <div class=""feature-icon"">👥</div>
                    <div>
                        <strong>Community:</strong> Connect with other readers and share your thoughts
                    </div>
                </div>
            </div>
            
            <div style=""text-align: center; margin: 30px 0;"">
                <a href=""#"" style=""background: linear-gradient(135deg, #667eea 0%, #764ba2 100%); color: white; padding: 15px 30px; text-decoration: none; border-radius: 25px; font-weight: bold; display: inline-block;"">Start Exploring</a>
            </div>
        </div>
        
        <div class=""footer"">
            <p><strong>Toshokan - Your Digital Library</strong></p>
            <p>Discover, read, and explore thousands of books</p>
            <p style=""margin-top: 20px; font-size: 12px; color: #999;"">
                Thank you for choosing Toshokan for your reading journey.
            </p>
        </div>
    </div>
</body>
</html>";
    }
} 