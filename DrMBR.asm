BITS 16
ORG 0x7C00  

start:
    mov si, message

print_loop:
    lodsb
    or al, al
    jz done
    mov ah, 0x0E  
    int 0x10
    jmp print_loop

done:
    cli
hang:
    hlt
    jmp hang

message db "Your PC has been trashed by DrPepper. Have a nice day. Also, stop fighting about Pepsi or sum shit.", 0

times 510-($-$$) db 0  
dw 0xAA55              
