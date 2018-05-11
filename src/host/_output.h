/*++
Copyright (c) Microsoft Corporation

Module Name:
- _output.h

Abstract:
- These methods provide processing of the text buffer into the final screen rendering state
- For all languages (CJK and Western)
- Most GDI work is processed in these classes

Author(s):
- KazuM Jun.11.1997

Revision History:
- Remove FE/Non-FE separation in preparation for refactoring. (MiNiksa, 2014)
--*/

#pragma once

#include "screenInfo.hpp"
#include "../buffer/out/OutputCell.hpp"

void StreamWriteToScreenBuffer(SCREEN_INFORMATION& screenInfo,
                               const std::wstring& wstr,
                               const bool fWasLineWrapped);

void WriteRectToScreenBuffer(SCREEN_INFORMATION& screenInfo,
                             const std::vector<std::vector<OutputCell>>& cells,
                             const COORD coordDest);

void WriteRegionToScreen(SCREEN_INFORMATION& screenInfo, _In_ PSMALL_RECT psrRegion);

void WriteToScreen(SCREEN_INFORMATION& screenInfo, const SMALL_RECT srRegion);

[[nodiscard]]
NTSTATUS WriteOutputString(SCREEN_INFORMATION& screenInfo,
                           _In_reads_(*pcRecords) const VOID * pvBuffer,
                           const COORD coordWrite,
                           const ULONG ulStringType,
                           _Inout_ PULONG pcRecords,    // this value is valid even for error cases
                           _Out_opt_ PULONG pcColumns);

[[nodiscard]]
NTSTATUS FillOutput(SCREEN_INFORMATION& screenInfo,
                    _In_ WORD wElement,
                    const COORD coordWrite,
                    const ULONG ulElementType,
                    _Inout_ PULONG pcElements); // this value is valid even for error cases

void FillRectangle(SCREEN_INFORMATION& screenInfo,
                   const OutputCell& cell,
                   const SMALL_RECT rect);
