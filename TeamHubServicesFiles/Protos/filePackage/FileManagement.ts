// Original file: Protos/file.proto

import type * as grpc from '@grpc/grpc-js'
import type { MethodDefinition } from '@grpc/proto-loader'
import type { DeleteRequest as _filePackage_DeleteRequest, DeleteRequest__Output as _filePackage_DeleteRequest__Output } from '../filePackage/DeleteRequest';
import type { DeleteResponse as _filePackage_DeleteResponse, DeleteResponse__Output as _filePackage_DeleteResponse__Output } from '../filePackage/DeleteResponse';
import type { DownloadRequest as _filePackage_DownloadRequest, DownloadRequest__Output as _filePackage_DownloadRequest__Output } from '../filePackage/DownloadRequest';
import type { DownloadResponse as _filePackage_DownloadResponse, DownloadResponse__Output as _filePackage_DownloadResponse__Output } from '../filePackage/DownloadResponse';
import type { FileRequest as _filePackage_FileRequest, FileRequest__Output as _filePackage_FileRequest__Output } from '../filePackage/FileRequest';
import type { FileResponse as _filePackage_FileResponse, FileResponse__Output as _filePackage_FileResponse__Output } from '../filePackage/FileResponse';

export interface FileManagementClient extends grpc.Client {
  DeleteFile(argument: _filePackage_DeleteRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  DeleteFile(argument: _filePackage_DeleteRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  DeleteFile(argument: _filePackage_DeleteRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  DeleteFile(argument: _filePackage_DeleteRequest, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  deleteFile(argument: _filePackage_DeleteRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  deleteFile(argument: _filePackage_DeleteRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  deleteFile(argument: _filePackage_DeleteRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  deleteFile(argument: _filePackage_DeleteRequest, callback: grpc.requestCallback<_filePackage_DeleteResponse__Output>): grpc.ClientUnaryCall;
  
  DownloadFile(argument: _filePackage_DownloadRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  DownloadFile(argument: _filePackage_DownloadRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  DownloadFile(argument: _filePackage_DownloadRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  DownloadFile(argument: _filePackage_DownloadRequest, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  downloadFile(argument: _filePackage_DownloadRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  downloadFile(argument: _filePackage_DownloadRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  downloadFile(argument: _filePackage_DownloadRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  downloadFile(argument: _filePackage_DownloadRequest, callback: grpc.requestCallback<_filePackage_DownloadResponse__Output>): grpc.ClientUnaryCall;
  
  SaveFile(argument: _filePackage_FileRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  SaveFile(argument: _filePackage_FileRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  SaveFile(argument: _filePackage_FileRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  SaveFile(argument: _filePackage_FileRequest, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  saveFile(argument: _filePackage_FileRequest, metadata: grpc.Metadata, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  saveFile(argument: _filePackage_FileRequest, metadata: grpc.Metadata, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  saveFile(argument: _filePackage_FileRequest, options: grpc.CallOptions, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  saveFile(argument: _filePackage_FileRequest, callback: grpc.requestCallback<_filePackage_FileResponse__Output>): grpc.ClientUnaryCall;
  
}

export interface FileManagementHandlers extends grpc.UntypedServiceImplementation {
  DeleteFile: grpc.handleUnaryCall<_filePackage_DeleteRequest__Output, _filePackage_DeleteResponse>;
  
  DownloadFile: grpc.handleUnaryCall<_filePackage_DownloadRequest__Output, _filePackage_DownloadResponse>;
  
  SaveFile: grpc.handleUnaryCall<_filePackage_FileRequest__Output, _filePackage_FileResponse>;
  
}

export interface FileManagementDefinition extends grpc.ServiceDefinition {
  DeleteFile: MethodDefinition<_filePackage_DeleteRequest, _filePackage_DeleteResponse, _filePackage_DeleteRequest__Output, _filePackage_DeleteResponse__Output>
  DownloadFile: MethodDefinition<_filePackage_DownloadRequest, _filePackage_DownloadResponse, _filePackage_DownloadRequest__Output, _filePackage_DownloadResponse__Output>
  SaveFile: MethodDefinition<_filePackage_FileRequest, _filePackage_FileResponse, _filePackage_FileRequest__Output, _filePackage_FileResponse__Output>
}
