import { Injectable } from "@angular/core";

@Injectable()
export class PasswordService {
 
    message: any;
    
    constructor() { }
    public validatePassword(password) {
        var errors = [
          { name: 'lowercase letter', min: 1, value: /[a-z]/.test(password) },
          { name: 'uppercase letter', min: 1, value: /[A-Z]/.test(password) },
          { name: 'numeric character', min: 1, value: /\d/.test(password) },
          { name: 'special character', min: 1, value: /[~`!#$%\^&*@+=\-\[\]\\';,/{}|\\":<>\?]/g.test(password) },
          { name: 'characters in total', min: 6, value: password.length > 5 }
        ]
        this.message = 'Password must have at least';
        errors.forEach(error => {
          if (!error.value)
            this.message += ` ${error.min} ${error.name},`
        });
        if (this.message.lastIndexOf(',') < 0) this.message = undefined;
        if (this.message) {
          var newMsg = this.replaceAt(this.message, this.message.lastIndexOf(','), '.');
          this.message = newMsg.lastIndexOf(',') > -1 ? this.replaceAt(newMsg, newMsg.lastIndexOf(','), ' and') : newMsg;
        }
        return this.message;
      }
    
      private replaceAt(str, index, chr) {
        if (index > str.length - 1) return str;
        return str.substr(0, index) + chr + str.substr(index + 1);
      }
}