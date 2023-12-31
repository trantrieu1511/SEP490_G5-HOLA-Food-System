import { Injectable } from '@angular/core';

@Injectable()
export class iFunction {
    storage: any = localStorage;

    getStorage(name: string): any {
        if (typeof (Storage) !== 'undefined') {
            if (this.storage.getItem(name)) {
                return this.storage.getItem(name);
            } else {
                return "";
            }
        } else {
            return "";
        }
    }

    setStorage(name: string, value: string) {
        if (typeof (Storage) !== 'undefined') {
            this.storage.setItem(name, value);
        }
    }

    //lấy giá trị cookies theo tên
    getCookie(name: string) {
        return localStorage.getItem(name);
    }

    deleteCookie(name: any) {
        this.setCookie(name, "", -1);
    }

    //Gán giá trị cho 1 cookie
    setCookie(name: string, value: string, expireDays: number, path: string = "") {
        let d: Date = new Date();
        d.setTime(d.getTime() + expireDays * 24 * 60 * 60 * 1000);
        let expires: string = "expires=" + d.toUTCString();
        document.cookie = name + "=" + value + "; " + expires + (path.length > 0 ? "; path=" + path : "");
    }

    //Focus vào 1 control dạng input nào đó (y/c html base phải có hàm focus)
    setFocus(id: string) {
        var el = document.getElementById(id);
        if (el) {
            (<any>el).focus();
        }
    }

    //Chọn tất cả dòng text của 1 input nào đó (y/c html base phải có hàm select text)
    setSelect(id: string) {
        var el = document.getElementById(id);
        if (el) {
            (<any>el).select();
        }
    }

    convertImageBase64toFile(base64: string, fileName: string) : File{
        // Base64 to byte          
        const byteCharacters = atob(base64);
        const byteNumbers = new Array(byteCharacters.length);
        for (let i = 0; i < byteCharacters.length; i++) {
            byteNumbers[i] = byteCharacters.charCodeAt(i);
        }
        const byteArray = new Uint8Array(byteNumbers);
        // byte -> blob
        //const blob = new Blob([byteArray]);
        const blob = new Blob([byteArray], { type: 'image/*' });
        //blob -> file
        return new File([blob], fileName, { type: blob.type, lastModified: Date.now() });
    }
}
