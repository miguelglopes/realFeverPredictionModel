// Copyright 2010-2014 Google
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#ifndef OR_TOOLS_SAT_CP_MODEL_UTILS_H_
#define OR_TOOLS_SAT_CP_MODEL_UTILS_H_

#include <algorithm>
#include <functional>
#include <string>
#include <unordered_set>
#include <vector>

#include "ortools/base/integral_types.h"
#include "ortools/base/logging.h"
#include "ortools/sat/cp_model.pb.h"
#include "ortools/util/sorted_interval_list.h"

namespace operations_research {
namespace sat {

// Small utility functions to deal with negative variable/literal references.
inline int NegatedRef(int ref) { return -ref - 1; }
inline int PositiveRef(int ref) { return std::max(ref, NegatedRef(ref)); }
inline bool RefIsPositive(int ref) { return ref >= 0; }

// Small utility functions to deal with half-reified constraints.
inline bool HasEnforcementLiteral(const ConstraintProto& ct) {
  return !ct.enforcement_literal().empty();
}
inline int EnforcementLiteral(const ConstraintProto& ct) {
  return ct.enforcement_literal(0);
}

// Collects all the references used by a constraint. This function is used in a
// few places to have a "generic" code dealing with constraints. Note that the
// enforcement_literal is NOT counted here.
//
// TODO(user): replace this by constant version of the Apply...() functions?
struct IndexReferences {
  std::unordered_set<int> variables;
  std::unordered_set<int> literals;
  std::unordered_set<int> intervals;
};
void AddReferencesUsedByConstraint(const ConstraintProto& ct,
                                   IndexReferences* output);

// Applies the given function to all variables/literals/intervals indices of the
// constraint. This function is used in a few places to have a "generic" code
// dealing with constraints.
void ApplyToAllVariableIndices(const std::function<void(int*)>& function,
                               ConstraintProto* ct);
void ApplyToAllLiteralIndices(const std::function<void(int*)>& function,
                              ConstraintProto* ct);
void ApplyToAllIntervalIndices(const std::function<void(int*)>& function,
                               ConstraintProto* ct);

// Returns the name of the ConstraintProto::ConstraintCase oneof enum.
// Note(user): There is no such function in the proto API as of 16/01/2017.
std::string ConstraintCaseName(ConstraintProto::ConstraintCase constraint_case);

// Returns true if a proto.domain() contain the given value.
// The domain is expected to be encoded as a sorted disjoint interval list.
template <typename ProtoWithDomain>
bool DomainInProtoContains(const ProtoWithDomain& proto, int64 value) {
  for (int i = 0; i < proto.domain_size(); i += 2) {
    if (value >= proto.domain(i) && value <= proto.domain(i + 1)) return true;
  }
  return false;
}

// Sets the domain field of a proto from a sorted interval list.
template <typename ProtoWithDomain>
void FillDomain(const std::vector<ClosedInterval>& domain,
                ProtoWithDomain* proto) {
  proto->clear_domain();
  CHECK(IntervalsAreSortedAndDisjoint(domain));
  for (const ClosedInterval& interval : domain) {
    proto->add_domain(interval.start);
    proto->add_domain(interval.end);
  }
}

// Extract a sorted interval list from the domain field of a proto.
template <typename ProtoWithDomain>
std::vector<ClosedInterval> ReadDomain(const ProtoWithDomain& proto) {
  std::vector<ClosedInterval> result;
  for (int i = 0; i < proto.domain_size(); i += 2) {
    result.push_back({proto.domain(i), proto.domain(i + 1)});
  }
  CHECK(IntervalsAreSortedAndDisjoint(result));
  return result;
}

// Returns the list of values in a given domain.
// This will fail if the domain contains more than one millions values.
template <typename ProtoWithDomain>
std::vector<int64> AllValuesInDomain(const ProtoWithDomain& proto) {
  std::vector<int64> result;
  for (int i = 0; i < proto.domain_size(); i += 2) {
    for (int64 v = proto.domain(i); v <= proto.domain(i + 1); ++v) {
      CHECK_LE(result.size(), 1e6);
      result.push_back(v);
    }
  }
  return result;
}

}  // namespace sat
}  // namespace operations_research

#endif  // OR_TOOLS_SAT_CP_MODEL_UTILS_H_
