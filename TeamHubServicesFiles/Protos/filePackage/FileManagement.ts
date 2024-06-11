// Original file: Protos/file.proto

import type * as grpc from '@grpc/grpc-js'
import type { MethodDefinition } from '@grpc/proto-loader'
import type { FileRequest as _filePackage_FileRequest, FileRequest__Output as _filePackage_FileRequest__Output } from '../filePackage/FileRequest';
import type { FileResponse as _filePackage_FileResponse, FileResponse__Output as _filePackage_FileResponse__Output } from '../filePackage/FileResponse';

export interface FileManagementClient extends grpc.Client {
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
  SaveFile: grpc.handleUnaryCall<_filePackage_FileRequest__Output, _filePackage_FileResponse>;
  
}

export interface FileManagementDefinition extends grpc.ServiceDefinition {
  SaveFile: MethodDefinition<_filePackage_FileRequest, _filePackage_FileResponse, _filePackage_FileRequest__Output, _filePackage_FileResponse__Output>
}
