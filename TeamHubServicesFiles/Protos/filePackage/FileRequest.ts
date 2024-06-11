// Original file: Protos/file.proto


export interface FileRequest {
  'projectName'?: (number);
  'fileName'?: (string);
  'extension'?: (string);
  'fileString'?: (Buffer | Uint8Array | string);
}

export interface FileRequest__Output {
  'projectName'?: (number);
  'fileName'?: (string);
  'extension'?: (string);
  'fileString'?: (Buffer);
}
