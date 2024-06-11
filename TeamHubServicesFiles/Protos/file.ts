import type * as grpc from '@grpc/grpc-js';
import type { MessageTypeDefinition } from '@grpc/proto-loader';

import type { FileManagementClient as _filePackage_FileManagementClient, FileManagementDefinition as _filePackage_FileManagementDefinition } from './filePackage/FileManagement';

type SubtypeConstructor<Constructor extends new (...args: any) => any, Subtype> = {
  new(...args: ConstructorParameters<Constructor>): Subtype;
};

export interface ProtoGrpcType {
  filePackage: {
    FileManagement: SubtypeConstructor<typeof grpc.Client, _filePackage_FileManagementClient> & { service: _filePackage_FileManagementDefinition }
    FileRequest: MessageTypeDefinition
    FileResponse: MessageTypeDefinition
  }
}

